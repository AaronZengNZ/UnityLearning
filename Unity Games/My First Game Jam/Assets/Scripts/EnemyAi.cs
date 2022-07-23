using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float stoppingDistance = 1f;
    Rigidbody2D rb;
    [SerializeField] Transform target;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float attackDamage = 25f;
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float damageInterval = 0.2f;
    [SerializeField] GameObject sprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSoundEffect;
    [SerializeField] bool isExplosive = false;
    [SerializeField] ParticleSystem deathParticles;
    Vector2 moveDirection;
    float HP;
    bool canBeDamaged = true;
    bool attackCooldown = false;
    bool isExploding = false;

    [SerializeField] float goldReward = 10;

    [SerializeField] CurrencyHolder currencyHolder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currencyHolder = FindObjectOfType<CurrencyHolder>();
        HP = maxHP;
        isExploding = false;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (canBeDamaged)
        {
            if (other.tag == "Bullets")
            {
                //Destroy(collision.gameObject);
                HP -= other.transform.parent.gameObject.GetComponent<GunScript>().damage;
                if (HP <= 0)
                {
                    if (currencyHolder != null)
                    {
                        currencyHolder.IncreaseGold(goldReward);
                    }
                    if (isExplosive) { StartCoroutine(Explosion()); return; }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                StartCoroutine(DamageInterval());
            }
        }
    }

    IEnumerator Explosion()
    {
        
        if (isExploding == false)
        {
            isExploding = true;
            deathParticles.Play();
            audioSource.PlayOneShot(deathSoundEffect, 1f);
            Destroy(sprite);
            if (Vector2.Distance(target.position, transform.position) < 4f)
            {
                target.gameObject.GetComponent<PlayerHealth>().TakeDamage(50);
            }
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }

    IEnumerator DamageInterval()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(damageInterval);
        canBeDamaged = true;
    }

    private void MoveTowardsPlayer()
    {
        if (Vector2.Distance(target.position, transform.position) < stoppingDistance) { rb.velocity = new Vector2(0, 0); StartCoroutine(AttackPlayer());  return; }
        Vector3 direction = (target.position - transform.position).normalized;
        //float angle = Mathf Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;

            sprite.transform.localScale = new Vector2(Mathf.Sign(direction.x) * -2.5f, 2.5f);

        moveDirection = direction;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }

    IEnumerator AttackPlayer()
    {
        if(isExplosive) { StartCoroutine(Explosion()); yield return new WaitForSeconds(0); }
        else if (attackCooldown) { yield return new WaitForSeconds(0); }
        else
        {
            attackCooldown = true;
            target.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            yield return new WaitForSeconds(attackInterval);
            attackCooldown = false;
        }
    }
}

