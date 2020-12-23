using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void processInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            
            rigidBody.AddRelativeForce(Vector3.up);

            if (!audioSource.isPlaying) // So it does not layer up.
                audioSource.Play();
        }

        else
        {
            audioSource.Stop();
        }
           

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(-Vector3.forward);

        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward);

    }
}
