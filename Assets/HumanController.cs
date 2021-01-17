using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;

//using UnityEditor.Animations;

public class HumanController : NetworkBehaviour
{
    public Animator animator;
    [FormerlySerializedAs("t")] public Transform transform;
    [FormerlySerializedAs("tHand")] public Transform transformHand;
    [FormerlySerializedAs("tRobot")] public Transform transformRobot;
    [FormerlySerializedAs("rb")] public Rigidbody humanRigidbody;
    private float distOld = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject human = gameObject;
        transform = human.transform;
        humanRigidbody = human.GetComponents<Rigidbody>()[0];
        transformHand = GameObject.Find("Bip001 R Hand").transform; 
        animator = human.GetComponent<Animator>();
        GameObject robot = GameObject.Find("yumiLimits");
        transformRobot = robot.transform;
        robot.GetComponents<MonoBehaviour>();
        RosSharp.RosBridgeClient.PoseStampedPublisherStatic.PublishedTransform = transformHand;
        //robot.GetComponents<MonoBehaviour>()[0].transform.position = transformHand.position;
        //robot.GetComponents<MonoBehaviour>()[0].transform.eulerAngles = transformHand.eulerAngles; 
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 1.0f;
        if (Vector3.Dot(transformRobot.position - transform.position, transform.forward) > -.5) {
            dist = norm(transformRobot.position - transformHand.position) - 10;
        }
        dist = Math.Min(Math.Max(dist, 0.0f), 1.0f);

        dist = .98f * distOld + dist * .02f;
        distOld = dist;
        animator.SetFloat("Hand", (1.0f - dist) / 1.0f);
        animator.SetFloat("Walk", (dist) / 1.0f);

        var humanVelocity = humanRigidbody.velocity;
        
        // obtain the human's speed from the velocity
        float humanSpeed = norm(humanVelocity);
        animator.speed = 0.15f * humanSpeed;

        if (!isLocalPlayer) {
            return;  // non-local players should not impact the human
        }
        
        float hyperScalar = 80 / (2.0f - dist);
        AddForcesToBodyForFromKeyboardDirections(humanRigidbody, hyperScalar);

        Vector3 groundVelocityVector = new Vector3(humanVelocity[0], 0, humanVelocity[2]);
        // add damping forces to human's movement
        humanRigidbody.AddForce(-10 * groundVelocityVector);
        // add damping forces to torque for the human
        humanRigidbody.AddTorque(-humanRigidbody.angularVelocity);
        // set the human's velocity vector
        var alignmentVelocityVector = Vector3.Dot(transform.forward, humanRigidbody.velocity);
        humanRigidbody.velocity = new Vector3(
            alignmentVelocityVector * transform.forward[0],
            humanVelocity[1],
            alignmentVelocityVector * transform.forward[2]
        );
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
