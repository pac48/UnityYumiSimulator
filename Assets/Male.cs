using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = System.Random;

//using UnityEditor.Animations;

public class Male : YumiMalePlayer
{
    public Animator animator;
    public Transform transform;
    public Transform transformHand;
    public Transform transformRobot;
    public GameObject bottle; 
    public Rigidbody humanRigidbody;
    private float distOld = 1;
	private Transform[] spawnPoints;
    private Transform goal;
    [SyncVar] public bool handRaised;
    [SyncVar] public bool holdingObject = false;
    [SyncVar] public Vector3 velocity;
    [SyncVar] public Vector3 position;
    [SyncVar] public Quaternion rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject human = gameObject;
        transform = human.transform;
        humanRigidbody = human.GetComponents<Rigidbody>()[0];
        transformHand = GameObject.Find("Bip001 R Finger2").transform;
        bottle = GameObject.Find("bottle");
        animator = human.GetComponent<Animator>();
        velocity = humanRigidbody.velocity;
        position = humanRigidbody.position;
        rotation = humanRigidbody.rotation;
        var spawner = GameObject.Find("spawnArray");
        spawnPoints = spawner.GetComponentsInChildren<Transform>();
        goal = GameObject.Find("goal").GetComponent<Transform>();
        //transform.position = goal.position;
        base.Start();
    }

    public override void PostAssignPlayer()
    {
        functions.Add(addRobot);
        Respawn();
    }

    void addRobot(){
        GameObject robot = GameObject.Find("effectorR");
        if (robot == null)
            return;
        transformRobot = robot.transform;
        robot.GetComponents<MonoBehaviour>();
        RosSharp.RosBridgeClient.PoseStampedPublisherStatic.PublishedTransform = transformHand;
        Debug.Log("robot added");
        functions.Remove(addRobot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
    }
    
    override public void FixedUpdateHuman()
    {
        CheckAndApplyGoalReset();
        AttemptHandover();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handRaised = !handRaised;
        }
        CommonUpdate();
        float dist = 1.0f;
        if (Vector3.Dot(transformRobot.position - transform.position, transform.forward) > -.5) {
                dist = norm(transformRobot.position - transformHand.position) - 5;
            }
        dist = Math.Min(Math.Max(dist, 0.0f), 1.0f);
        float hyperScalar = 80 / (2.0f - dist);
        var humanVelocity = humanRigidbody.velocity;
        Vector3 groundVelocityVector = new Vector3(humanVelocity[0], 0, humanVelocity[2]);
        AddForcesToBodyForFromKeyboardDirections(humanRigidbody, hyperScalar);
        // add damping forces to human's movement
        humanRigidbody.AddForce(-10 * groundVelocityVector);
        // add damping forces to torque for the human
        humanRigidbody.AddTorque(-humanRigidbody.angularVelocity);
        // set the human's velocity vector
        var alignmentVelocityVector = Vector3.Dot(transform.forward, humanRigidbody.velocity);
        humanRigidbody.velocity = new Vector3(
            alignmentVelocityVector * transform.forward[0],
            humanVelocity[1],
            alignmentVelocityVector * transform.forward[2]);
        CmdPosVelRotHand(humanRigidbody.position, humanRigidbody.velocity, 
            humanRigidbody.rotation, handRaised, holdingObject);
    }
    
    
    private static readonly Random getrandom = new Random();
    void CheckAndApplyGoalReset()
    {
        if (!holdingObject || Max(Abs(goal.position - transform.position)) > 1.0)
            return;
        Respawn();
    }
    
    void Respawn(){
        int num = getrandom.Next(1, spawnPoints.Length);
        humanRigidbody.position = spawnPoints[num].position;
        holdingObject = false;
        handRaised = false;
    }
    
    void AttemptHandover()
    {
        if (Max(Abs(transformRobot.position - transformHand.position)) > 1.0)
            return;
        holdingObject = true;
    }
    
    Vector3 Abs(Vector3 v) {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z) );
    }
    float Max(Vector3 v) {
        return Math.Max(Math.Max(v.x, v.y), v.z);
    }
    
    [Command]
    void CmdPosVelRotHand(Vector3 positionCmd, Vector3 velocityCmd, Quaternion rotationCmd, bool handRaisedCmd,
        bool holdingObjectCmd)
    {
        velocity = velocityCmd;
        position = positionCmd;
        rotation = rotationCmd;
        handRaised = handRaisedCmd;
        holdingObject = holdingObjectCmd;
    }
    override public void FixedUpdateYumi() {
        CommonUpdate();
        humanRigidbody.velocity = velocity;
        humanRigidbody.position = position;
        humanRigidbody.rotation = rotation;
    }
    override public void FixedUpdateServer()
    {
        CommonUpdate();
        humanRigidbody.velocity = velocity;
        position = humanRigidbody.position = position;
        rotation = humanRigidbody.rotation = rotation;
    }


   void CommonUpdate(){
       var humanVelocity = humanRigidbody.velocity;
       // obtain the human's speed from the velocity
       float humanSpeed = norm(humanVelocity);
       animator.speed = 0.15f * humanSpeed;
       if (transformRobot == null)
           return;
        int val = handRaised ? 0 : 1;
        float  dist = .9f * distOld + val * .1f;
        distOld = dist;
        animator.SetFloat("Hand", (1.0f - dist) / 1.0f);
        animator.SetFloat("Walk", (dist) / 1.0f);
        if (!holdingObject){
            bottle.transform.position = GameObject.Find("effectorR").transform.position;
            bottle.transform.rotation = GameObject.Find("effectorR").transform.rotation;
        } else{
            bottle.transform.position = transformHand.position;
            bottle.transform.rotation = transformHand.rotation;
        }
   }

    float norm(Vector3 vel) {
        return Mathf.Sqrt(vel[0] * vel[0] + vel[2] * vel[2]);
    }

    void AddForcesToBodyForFromKeyboardDirections(Rigidbody rigidbody, float val) {
        if (Input.GetKey(KeyCode.UpArrow)) {
            rigidbody.AddForce(transform.forward * val);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rigidbody.AddForce(-transform.forward * val);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rigidbody.AddTorque(new Vector3(0, 3, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody.AddTorque(new Vector3(0, -3, 0));
        }
    }
}
