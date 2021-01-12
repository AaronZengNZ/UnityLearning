using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool isExplodingProjectile = false;
    [SerializeField] GameObject deathVFX;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (!isExplodingProjectile)
        {
            Destroy(gameObject);
        }
    }

}
