using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform transform;
    public ArticulationBody rb;
    public Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(transform.position);
        Matrix4x4 m = cam.projectionMatrix;
        Vector3 vec2 = m * transform.position;
        vec2.z = transform.position.z;
       // Debug.Log(vec2);
        Vector3 vec1 = Input.mousePosition;
        vec1.x -= .5f*Screen.width;
        vec1.y -= .5f * Screen.height;
        vec1.x /= .5f*Screen.width;
        vec1.y /= .5f * Screen.height;
        Vector3 force = vec1 - vec2;
        //Debug.Log(vec1);
        force.x = force.x * 900f - 15*rb.velocity.x;
        force.y = force.y * 900f - 15 * rb.velocity.y;
        force.z = force.z * 90f - .15f * rb.velocity.z;
        rb.AddForce(force);
    }
}
