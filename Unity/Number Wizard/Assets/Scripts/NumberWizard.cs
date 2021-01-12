using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] TextMeshProUGUI guessText;
    int guess;
    void StartGame()
    {
        NextGuess();
    }

    void NextGuess()
    {
        guess = (min + max) / 2;
        guess = Random.Range(min, max + 1);
        guessText.text = guess.ToString();
    }

    void Start()
    {
        StartGame();
    }

    public void OnPressHigher()
    {
        min = guess + 1;
        NextGuess();
    }

    public void OnPressLower()
    {
        max = guess - 1;
        NextGuess();
    }
}
