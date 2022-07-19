using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] ParticleSystem deathParticles;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints <= 0f)
        {

            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
