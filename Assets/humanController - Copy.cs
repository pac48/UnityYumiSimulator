using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//using UnityEditor.Animations;

public class humanController : NetworkBehaviour
{
    public Animator animator;
    public Transform t;
    public Transform tHand;
    public Transform tRobot;
    public Rigidbody rb;
    private float distOld = 2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject human = this.gameObject;
        t = human.transform;
        rb = human.GetComponents<Rigidbody>()[0];
        tHand = human.transform; //.Find("Bip001 R Hand");
        animator = human.GetComponent<Animator>();
        GameObject robot = GameObject.Find("yumiLimits");
        tRobot = robot.transform;//.Find("yumiLimits");
        Debug.Log("esrh");
        
    }

    // Update is called once per frame
    void Update()
    {
       
            float dist = 1.0f;
            if (Vector3.Dot(tRobot.position - t.position, t.forward) > -.5)
                dist = norm(tRobot.position - tHand.position) - 10;
            if (dist > 1.0f)
                dist = 1.0f;
            else if (dist < 0.0f)
                dist = 0.0f;
            dist = .98f * distOld + dist * .02f;
            distOld = dist;
            animator.SetFloat("Hand", (1.0f - dist) / 1.0f);
            animator.SetFloat("Walk", (dist) / 1.0f);
            float speed = norm(rb.velocity);
            animator.speed = 0.15f * speed;
            if (isLocalPlayer)
            {
            float val = 80 / (2.0f - dist);
            float alpha = 10;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(t.forward * val);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(-t.forward * val);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddTorque(new Vector3(0, 3, 0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddTorque(new Vector3(0, -3, 0));
            }

            Vector3 tmp = new Vector3(rb.velocity[0], 0, rb.velocity[2]);
            rb.AddForce(-alpha * tmp);
            rb.AddTorque(-rb.angularVelocity);
            val = Vector3.Dot(t.forward, rb.velocity);
            rb.velocity = new Vector3(val * t.forward[0], rb.velocity[1], val * t.forward[2]);
        }
    }

    float norm(Vector3 vel) {
        return Mathf.Sqrt(vel[0] * vel[0] + vel[2] * vel[2]);
    }
}
