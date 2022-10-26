using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CurrencyHolder : MonoBehaviour
{
    [SerializeField] float goldAmount = 0;
    [SerializeField] TextMeshProUGUI goldLabel;
    [SerializeField] float enemyAmount = 1;
    bool loadingScene = false;
    public float CurrentGold { get { return goldAmount; } }
    [SerializeField] string currentTwist;
    [SerializeField] string twist2;
    GameObject gameOverCanvas;
    bool hasChecked = false;

    void Awake()
    {
        goldLabel = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        goldLabel.text = goldAmount.ToString();
        int gameStatusCount = FindObjectsOfType<CurrencyHolder>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            hasChecked = true;
        }
    }

    private void Update()
    {
        if(gameOverCanvas == null && hasChecked == true)
        {
            gameOverCanvas = GameObject.Find("Game Over Canvas");
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(false);
            }
        }
        if (goldLabel == null)
        {
            goldLabel = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
            if(goldLabel.text != goldAmount.ToString())
            {
                goldLabel.text = goldAmount.ToString();
            }
        }
        if (FindObjectsOfType<EnemyAi>().Length <= 0 && loadingScene == false)
        {
            if (loadingScene == false)
            {
                if (FindObjectsOfType<ButtonManager>().Length > 0) { return; }
                NextScene();
            }
            
        }
    }

    public void AddTwist(string twist)
    {
        AddTwist(twist, 1);
    }

    public void AddTwist(string twist, int map)
    {
        if(twist == "respawn")
        {
            Replay();
            return;
        }
        if(twist == "main menu")
        {
            MainMenu();
            return;
        }
        Debug.Log("edited twist to " + twist);
        if (map == 1)
        {
            currentTwist = twist;
        }
        else if (map == 2)
        {
            twist2 = twist;
        }
    }

    public string GetTwist()
    {
        return GetTwist(1);
    }

    public string GetTwist(int map)
    {
        if (map == 1)
        {
            return currentTwist;
        }
        else if (map == 2)
        {
            return twist2;
        }
        else
        {
            return null;
        }
    }

    public void IncreaseGold(float amount)
    {
        goldAmount += Mathf.Abs(amount);
        goldLabel.text = goldAmount.ToString();
    }

    public void DecreaseGold(float amount)
    {
        goldAmount -= Mathf.Abs(amount);
        goldLabel.text = goldAmount.ToString();
    }

    public void EnemyKilled()
    {

    }

    public void ReloadScene()
    {
        loadingScene = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
        StartCoroutine(WaitAndDisable());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void NextScene()
    {

        StartCoroutine(NextSceneTransition());


    }

    public void FNextScene()
    {

        StartCoroutine(FastNextSceneTransition());


    }

    IEnumerator NextSceneTransition()
    {
        if (loadingScene == false)
        {
            loadingScene = true;
            Scene currentScene = SceneManager.GetActiveScene();
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f;
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            yield return new WaitForSeconds(1f);
            loadingScene = false;
        }
        yield return new WaitForSeconds(0f);
        
    }

    IEnumerator FastNextSceneTransition()
    {
        if (loadingScene == false)
        {
            loadingScene = true;
            Scene currentScene = SceneManager.GetActiveScene();
            yield return new WaitForSecondsRealtime(0.1f);
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            Time.timeScale = 1f;
            yield return new WaitForSeconds(1f);
            loadingScene = false;
        }
        yield return new WaitForSeconds(0f);

    }

    IEnumerator WaitAndDisable()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        loadingScene = false;

    }
}
