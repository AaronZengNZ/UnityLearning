using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 10;
    [SerializeField] int currentHP = 0;
    [SerializeField] TextMeshPro label;
    Enemy enemy;
    [SerializeField] PlayerGun playerGun;
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
    }

    void ProcessHit()
    {
        int damage = playerGun.gunDamage;
        if (playerGun == null) { damage = 1; }
        currentHP -= damage;
        if(currentHP <= 0)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
        }
        label.text = currentHP + "/" + maxHP;
    }
}
