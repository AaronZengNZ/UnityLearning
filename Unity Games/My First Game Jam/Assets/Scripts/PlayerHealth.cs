using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHP = 100;
    float currentHP;
    [SerializeField] TextMeshProUGUI healthLabel;

    [SerializeField] CurrencyHolder currencyHolder;

    [SerializeField] float damageInterval = .2f;

    [SerializeField] AudioSource hurtSfx;

    [SerializeField] GameObject hurtFlash;

    [SerializeField] Image HealthBar;

    bool canBeDamaged = true;

    float selfDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currencyHolder = FindObjectOfType<CurrencyHolder>();
        healthLabel.text = currentHP.ToString() + " / " + maxHP.ToString();
        hurtFlash.SetActive(false);
    }

    void Update()
    {
        if(HealthBar != null)
        {
            HealthBar.fillAmount = currentHP / maxHP;
        }
        else
        {
            HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        }
        if(currencyHolder == null)
        {
            currencyHolder = FindObjectOfType<CurrencyHolder>();
        }
    }

    public void SetSelfDamage(float amount)
    {
        selfDamage = amount;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (canBeDamaged)
        {
            if (other.tag == "Enemy Bullets")
            {
                TakeDamage(10);
            }
            else
            {
                TakeDamage(selfDamage);
            }
            StartCoroutine(DamageInterval());
        }
    }

    public void SetHealth(float amount)
    {
        maxHP = amount;
        currentHP = amount;
        healthLabel.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    IEnumerator DamageInterval()
    {
        canBeDamaged = false;
        hurtFlash.SetActive(true);
        yield return new WaitForSeconds(damageInterval);
        hurtFlash.SetActive(false);
        canBeDamaged = true;
    }

    public void TakeDamage(float amount)
    {
        hurtSfx.Play();
        currentHP -= amount;
        healthLabel.text = currentHP.ToString() + " / " + maxHP.ToString();
        StartCoroutine(DamageInterval());
        if(currentHP <= 0)
        {
            if (currencyHolder != null)
            {
                currencyHolder.ReloadScene();
                GetComponent<PlayerMovement>().moveSpeed = 0;
            }
        }
    }
}
