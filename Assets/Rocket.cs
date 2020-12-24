using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 100f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;

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
            RespondToThrustInput();
            RespondToRotationInput();
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState != States.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }   

    private void StartSuccessSequence()
    {
        audioSource.Stop();        
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        Invoke("LoadNextLevel", 2f);
    }

    private void StartDeathSequence()
    {
        audioSource.Stop();
        currentState = States.Dying;
        audioSource.PlayOneShot(explosionSound);
        explosionParticle.Play();
        Invoke("LoadLevelAfterCollision", 2f);
    }
    
    private void LoadNextLevel()
    {        
        SceneManager.LoadScene(1);
    }

    private void LoadLevelAfterCollision()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        float thrustPerFrame = thrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust(thrustPerFrame);
        }

        else
        {
            audioSource.Stop();
            thrustParticle.Stop();
        }
    }

    private void ApplyThrust(float thrustPerFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustPerFrame);

        if (!audioSource.isPlaying) // So it does not layer up.
            audioSource.PlayOneShot(thrustSound);

        thrustParticle.Play();
    }

    private void RespondToRotationInput()
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
