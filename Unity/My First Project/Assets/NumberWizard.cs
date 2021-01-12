using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    // Start is called before the first frame update
    int max;
    int min;
    int guess;
    void StartGame()
    {
        max = 1001;
        min = 1;
        guess = 500;
        Debug.Log("=======Welcome to number nerd...=======");
        Debug.Log("Think of a number in your mind, don't tell me (you cant)");
        Debug.Log("Highest is " + (max - 1) + ", Lowest is " + min + "...");
        Debug.Log("Tell me if your dumb number is higher or lower than " + guess);
        Debug.Log("Up Arrow Key = Higher, Down Arrow Key = Lower, Enter Key = Correct...?");
    }

    void NextGuess()
    {
        guess = (min + max) / 2;
        if (max - min <= 2)
        {
            Debug.Log("It's " + guess + "! (Press enter to play again)");
        }
        else
        {
            Debug.Log(guess + "?");
        };
    }

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            min = guess;
            NextGuess();
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            max = guess;
            NextGuess();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Yayhoo, me genius");
            StartGame();
        }
    }
}
