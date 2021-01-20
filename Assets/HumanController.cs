using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;

//using UnityEditor.Animations;

public class HumanController : YumiMalePlayer
{
    public Animator animator;
    public Transform transform;
    public Transform transformHand;
    public Transform transformRobot;
    public Rigidbody humanRigidbody;
    private float distOld = 1;
	public GameObject player;
    [SyncVar] public Vector3 velocity;
    [SyncVar] public Vector3 position;
    [SyncVar] public Quaternion rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject human = gameObject;
        transform = human.transform;
        humanRigidbody = human.GetComponents<Rigidbody>()[0];
        transformHand = GameObject.Find("Bip001 R Hand").transform; 
        animator = human.GetComponent<Animator>();
        velocity = humanRigidbody.velocity;
        position = humanRigidbody.position;
        rotation = humanRigidbody.rotation;
        base.Start();
      
    }

    public override void PostAssignPlayer()
    {
        functions.Add(addRobot);
    }

    void addRobot(){
        GameObject robot = GameObject.Find("yumiNetwork(Clone)");
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
    
    override public void FixedUpdateHuman(){
        CommonUpdate();
        float hyperScalar = 80 / (2.0f - distOld);
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
        velocity = humanRigidbody.velocity;
        position = humanRigidbody.position;
        rotation = humanRigidbody.rotation;
    }


   void CommonUpdate(){
       var humanVelocity = humanRigidbody.velocity;
       // obtain the human's speed from the velocity
       float humanSpeed = norm(humanVelocity);
       animator.speed = 0.15f * humanSpeed;
       if (transformRobot == null)
           return;
       float dist = 1.0f;
        if (Vector3.Dot(transformRobot.position - transform.position, transform.forward) > -.5) {
            dist = norm(transformRobot.position - transformHand.position) - 10;
        }
        dist = Math.Min(Math.Max(dist, 0.0f), 1.0f);
        dist = .98f * distOld + dist * .02f;
        distOld = dist;
        animator.SetFloat("Hand", (1.0f - dist) / 1.0f);
        animator.SetFloat("Walk", (dist) / 1.0f);
        

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
