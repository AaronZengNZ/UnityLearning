using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip victory;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(StartCrashhSequence());

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("working, friendly");
                break;
            case "Finish":
                StartCoroutine(StartFinishSequence());
                break;
            case "Fuel":
                Debug.Log("working, fuel");
                break;
            default:
                StartCoroutine(StartCrashSequence());
                break;
        }
    }

    IEnumerator StartFinishSequence()
    {
        yield return new WaitForSeconds(0f);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victory);
        victoryParticles.Play();
        Invoke("NextLevel", levelLoadDelay);
    }

    IEnumerator StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        yield return new WaitForSeconds(0f);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    IEnumerator StartCrashhSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        yield return new WaitForSeconds(0.2f);
        crashParticles.Play();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    { 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
