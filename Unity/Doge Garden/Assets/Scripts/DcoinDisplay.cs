using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DcoinDisplay : MonoBehaviour
{
    [SerializeField] int doggcoins = 100;
    Text dcoinText;

    void Start()
    {
        dcoinText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        dcoinText.text = doggcoins.ToString();
    }

    public bool HaveEnoughDcoins(int amount)
    {
        return doggcoins >= amount;
    }

    public void AddDcoins(int amount)
    {
        doggcoins += amount;
        UpdateDisplay();
    }

    public void SpendDcoins(int amount)
    {
        if (doggcoins >= amount)
        {
            doggcoins -= amount;
            UpdateDisplay();
        }
    }
}
