using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 400;
    [SerializeField] TextMeshPro label;

    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = startingBalance;
        label.text = currentBalance.ToString();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        label.text = currentBalance.ToString();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        label.text = currentBalance.ToString();

        if(currentBalance <= -1000)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
