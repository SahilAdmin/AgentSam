using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 100f;
    [SerializeField] float levelLoadDelay;

    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;    

    enum States { Alive, Transcending1, Transcending2, Dying};  // todo remove level2
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

     void OnCollisionEnter(Collision collision)
    {
        if (currentState != States.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;

            case "Finish":
                print("finish");
                StartSuccessSequence();
                break;

            case "Finish2":                   // todo remove finish 2.
                StartSuccessSeqence2();
                break;


            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSeqence2()            // todo remove startsequence 2
    {
        currentState = States.Transcending2;
        thrustParticle.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        Invoke("LoadNextLevel1", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        currentState = States.Transcending1;
        thrustParticle.Stop();
        audioSource.Stop();        
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        currentState = States.Dying;
        audioSource.Stop();
        thrustParticle.Stop();
        audioSource.PlayOneShot(explosionSound);
        explosionParticle.Play();
        Invoke("LoadLevelAfterCollision", levelLoadDelay);
    }

    private void LoadNextLevel1()
    {
        SceneManager.LoadScene(2);
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

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
        }

        else
        {
            audioSource.Stop();
            thrustParticle.Stop();
        }
    }

    private void ApplyThrust()
    {        
        float thrustPerFrame = thrust * Time.deltaTime;
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
