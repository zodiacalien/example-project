using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigidBody;

    [SerializeField] float rcsThrust = 5f;
    [SerializeField] float forwardThrust = 5f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip loadSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathClipParticles;
    [SerializeField] ParticleSystem loadSoundParticles;


    enum State {Alive, Dying, Finish};
    State state = State.Alive;  

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (state == State.Alive)
        {
            RespondToRotate();
            RespondToThrust();
        }
    }
    void OnCollisionEnter(Collision collision)
    {    
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                print("OK");
                break;
            case "Enemy":
                DeathFunction();
                break;
            case "Finish":
                FinishFunction();
                break;
        }
    }


    private void FinishFunction()
    {
        state = State.Finish;
        audioSource.Stop();
        audioSource.PlayOneShot(loadSound);
        loadSoundParticles.Play();
        Invoke("NextLevel1", 2f);
        print("Good one forsen!");
    }

    private void DeathFunction()
    {
        print("DeadLul");
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        deathClipParticles.Play(); 
        Invoke("RespawnPlayer", 2f); //parameretise time
    }


    private void NextLevel1()
    {
        SceneManager.LoadScene(1);
    
    }

    private void RespawnPlayer()
    {
        SceneManager.LoadScene(0);
    }


    private void RespondToRotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rcsThrust);

        }
        else if (Input.GetKey(KeyCode.D))

        {
            transform.Rotate(-Vector3.forward * rcsThrust);

        }
        rigidBody.freezeRotation = false; //resume physics control of rotation 
    }

    private void RespondToThrust()
    {
        float rotationThisFrame = forwardThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * forwardThrust);
        if (!audioSource.isPlaying)
        {
            mainEngineParticles.Play();
            audioSource.PlayOneShot(mainEngine);
        }
       
    }
}
