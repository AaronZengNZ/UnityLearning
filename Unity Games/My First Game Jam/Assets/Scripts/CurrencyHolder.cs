using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CurrencyHolder : MonoBehaviour
{
    [SerializeField] float goldAmount = 0;
    [SerializeField] TextMeshProUGUI goldLabel;
    public float CurrentGold { get { return goldAmount; } }

    void Awake()
    {
        goldLabel.text = goldAmount.ToString();
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

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
