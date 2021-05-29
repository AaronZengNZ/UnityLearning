using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamesession : MonoBehaviour
{

    [SerializeField] int playerLives = 10;
    [SerializeField] int score = 0;

    [SerializeField] float respawnDelay = 2f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<Gamesession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            ResetGameSession();
        }
    }

    IEnumerator TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        livesText.text = playerLives.ToString();
        yield return new WaitForSecondsRealtime(respawnDelay);
        SceneManager.LoadScene(currentSceneIndex);

    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
