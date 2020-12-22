using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void processInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
            rigidBody.AddRelativeForce(Vector3.up);

        if (Input.GetKey(KeyCode.RightArrow))
            print("Rotating right ");

        else if (Input.GetKey(KeyCode.LeftArrow))
            print("Rotating left");

    }
}
