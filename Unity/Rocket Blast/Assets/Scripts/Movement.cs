using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float turnSpeed = 90f;
    [SerializeField] float reloadGoal = 3f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem engineParticles;

    float reload = 0f;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        if(reload >= reloadGoal)
        {
            
        }
        else
        {
            reload += Time.deltaTime * 3;
        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && reload >= 0)
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustForce);
            reload -= Time.deltaTime;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!engineParticles.isPlaying)
            {
                engineParticles.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                engineParticles.Stop();
            }
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(turnSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        { 
            ApplyRotation(-turnSpeed);
        }
 
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
