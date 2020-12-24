using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 100f;

    enum States { Alive, Trancending, Dying};
    States currentState = States.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == States.Alive)
        {
            Thrust();
            Rotate();
        }

        else
            audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;

            case "Finish":
                currentState = States.Trancending;
                Invoke("LoadNextLevel", 2f);
                break;

            default:
                currentState = States.Dying;
                Invoke("LoadLevelAfterCollision", 2f);
                break;
        }
    }
    
    private void LoadLevelAfterCollision()
    {        
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {        
        SceneManager.LoadScene(1);
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
