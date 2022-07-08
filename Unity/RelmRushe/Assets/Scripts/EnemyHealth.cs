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
    [SerializeField] int difficultyRound = 500;
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
        int damage = playerGun.gunDamage;
        if (playerGun == null) { currentHP -= -1f; }
        else
        {
            currentHP -= (float)damage;
        }
        
        if (damage > 0) { deathfx.Play(); }
        if (currentHP <= 0)
        {
            enemy.RewardGold();
            maxHP += difficultyRound;
            
            gameObject.SetActive(false);
        }
        label.text = currentHP + "/" + maxHP;
    }
}
