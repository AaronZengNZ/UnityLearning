using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Enemy))] 
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 4;
    float currentHP = 0;
    [SerializeField] TextMeshPro label;
    Enemy enemy;
    [SerializeField] PlayerGun playerGun;
    [SerializeField] ParticleSystem deathfx;
    [Tooltip("Enemy hp incremental amount")]
    [SerializeField] int difficultyRound = 2;
    [SerializeField] ParticleSystem explosion;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHP = maxHP;
        label.text = currentHP + "/" + maxHP;
    }

    void Start() {
        enemy = this.GetComponent<Enemy>();
        playerGun = FindObjectOfType<PlayerGun>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        Debug.Log("hit");
        if(other.tag == "Magic Ball")
        {
            currentHP -= 1f;
        }
        if(other.tag == "Cannonball")
        {
            explosion.Play();
        }
    }

    void ProcessHit()
    {
        Debug.Log("processed hit");
        if (playerGun != null) 
        {
            int damage = playerGun.gunDamage;
            currentHP -= (float)damage; 
        }
        else
        {
            currentHP -= 1;
        }
        deathfx.Play();
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            enemy.RewardGold();
            maxHP += difficultyRound;

            gameObject.SetActive(false);
        }
        label.text = currentHP + "/" + maxHP;

    }
}
