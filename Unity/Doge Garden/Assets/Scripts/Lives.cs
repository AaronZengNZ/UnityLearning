using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] float baseLives = 11;
    float lives;
    Text livesText;

    void Start()
    {
        if(PlayerPrefsController.GetDifficulty() <= 0f)
        {
            lives = 10f;
        }
        else if (PlayerPrefsController.GetDifficulty() <= 1f)
        {
            lives = 3f;
        }
        else if(PlayerPrefsController.GetDifficulty() <= 2f)
        {
            lives = 1f;
        }
        else
        {
            lives = 1f;
        }
        livesText = GetComponent<Text>();
        UpdateDisplay();
        Debug.Log("difficulty setting currently is " + PlayerPrefsController.GetDifficulty());
    }

    private void UpdateDisplay()
    {
        livesText.text = lives.ToString();
    }

    public void TakeLife(int amount)
    {
        lives -= amount;
        UpdateDisplay();

        if(lives <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }
}
