using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour 
{

    Rigidbody frigidbody;

    // Makes objects float up & down while gently spinning.
    // User Inputs

    public float amplitude = 0.5f;
    public float frequency = 0.5f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        frigidbody = GetComponent<Rigidbody>();
        // Store the starting position & rotation of the object
        //posOffset = transform.position;
    }
    private void FixedUpdate()
    {
        frigidbody.AddForce(0, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        // Float up/down with a Sin()
       // tempPos = posOffset;
       // tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

      //  transform.position = tempPos;
    }

}

