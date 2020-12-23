using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 100f;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        float thrustPerFrame = thrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustPerFrame);

            if (!audioSource.isPlaying) // So it does not layer up.
                audioSource.Play();
        }

        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        float rotationPerFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.freezeRotation = true;     // Take manual control of rotation.
            transform.Rotate(-Vector3.forward * rotationPerFrame);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.freezeRotation = true;     // Take manual control of rotation.
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }

        rigidBody.freezeRotation = false;         // Resume physics control of rotation.
    }

    
}
