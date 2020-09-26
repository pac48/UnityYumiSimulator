using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class fade : MonoBehaviour
{
    public Image im;
    private float x = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = im.color;
        x += 0.01f;
        color.a = math.sin(x);
        im.color = color;

    }
}
