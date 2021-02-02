using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float North = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        North = 10.0f;

        if(Input.GetKeyDown(KeyCode.W))
        {
            print("forward");
        }
    }
}
