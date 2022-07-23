using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHP = 100;
    float currentHP;
    [SerializeField] TextMeshProUGUI healthLabel;

    [SerializeField] CurrencyHolder currencyHolder;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currencyHolder = FindObjectOfType<CurrencyHolder>();
        healthLabel.text = currentHP.ToString() + " HP";
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(20);
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        healthLabel.text = currentHP.ToString() + " HP";
        if(currentHP <= 0)
        {
            if (currencyHolder != null)
            {
                currencyHolder.ReloadScene();
            }
        }
    }
}
