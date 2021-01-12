using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeBallBoingScript : MonoBehaviour
{
    [SerializeField] AudioClip[] ballSounds;
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
        myAudioSource.PlayOneShot(clip);
    }
}
