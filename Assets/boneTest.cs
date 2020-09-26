using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class boneTest : MonoBehaviour
{
    public BoneColliders colliders;
   
        // Start is called before the first frame update
        

    void Start()
    {
        for (int i = 1; i <= 7; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                BoxCollider2D bi = colliders.Get(i);
                BoxCollider2D bj = colliders.Get(j);
               // Physics2D.IgnoreCollision(bi, bj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
[System.Serializable]
public class BoneColliders
{
    public BoxCollider2D c1;
    public BoxCollider2D c2;
    public BoxCollider2D c3;
    public BoxCollider2D c4;
    public BoxCollider2D c5;
    public BoxCollider2D c6;
    public BoxCollider2D c7;

    public BoxCollider2D Get(int c)
    {
        if (c == 1) {
            return c1;
        } else if (c == 2) {
            return c2;
        }
        else if (c == 3)
        {
            return c3;
        }
        else if (c == 4)
        {
            return c4;
        }
        else if (c == 5)
        {
            return c5;
        }
        else if (c == 6)
        {
            return c6;

        }
        else
        {
            return c7;
        }
    
    }
 }