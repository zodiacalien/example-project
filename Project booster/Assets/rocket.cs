using UnityEngine;
using UnityEngine.SceneManagement; 

public class rocket : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigidBody;
    [SerializeField] float rcsThrust = 5f;
    [SerializeField] float forwardThrust = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Enemy":
                print("DeadLul");
                break;
        }
    }
    private void Rotate()
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

    private void Thrust()
    {
        float rotationThisFrame = forwardThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * forwardThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
