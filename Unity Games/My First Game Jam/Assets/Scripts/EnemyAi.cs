using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float stoppingDistance = 1f;
    Rigidbody2D rb;
    [SerializeField] Transform target;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float attackDamage = 25f;
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float damageInterval = 0.1f;
    [SerializeField] GameObject sprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSoundEffect;
    [SerializeField] bool isExplosive = false;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] float enableDelay = 0f;
    [SerializeField] AudioSource hurtSfx;
    [SerializeField] bool slippery = false;
    [SerializeField] GameObject explosionFlash;
    [SerializeField] bool isBossFight = false;
    [SerializeField] Image HealthBar;
    [SerializeField] TextMeshProUGUI HealthLabel;
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] ParticleSystem gunone;
    [SerializeField] ParticleSystem guntwo;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Image healthBar1;
    [SerializeField] Image healthBar2;
    [SerializeField] TextMeshProUGUI enemyHealthLabel;
    bool isBossing = false;
    Vector2 moveDirection;
    float HP;
    bool canBeDamaged = true;
    bool attackCooldown = false;
    bool isExploding = false;
    public Animator animator;


    [SerializeField] float goldReward = 10;

    [SerializeField] CurrencyHolder currencyHolder;

    private void Awake()
    {
        if (explosionFlash != null)
        {
            explosionFlash.SetActive(false);
        }
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currencyHolder = FindObjectOfType<CurrencyHolder>();
        HP = maxHP;
        isExploding = false;
        if (enableDelay > 0)
        {
            StartCoroutine(delayMovement());
        }
        attackCooldown = false;
        isBossing = false;
        if (isBossFight)
        {
            StartCoroutine(Boss());
        }
    }

    IEnumerator delayMovement()
    {
        float previousSpeed = speed;
        speed = 0;
        SetHealthBar(false);
        yield return new WaitForSeconds(enableDelay);
        SetHealthBar(true);
        speed = previousSpeed;
    }

    private void SetHealthBar(bool set)
    {
        if (healthBar1 != null && healthBar2 != null)
        {
            healthBar1.gameObject.SetActive(set);
            healthBar2.gameObject.SetActive(set);
        }
        enemyHealthLabel.gameObject.SetActive(set);
    }

    private void Update()
    {
        MoveTowardsPlayer();
        UpdateHealthBar();
        UseGun();
        DoHealthBar();
    }

    private void DoHealthBar()
    {
        if(healthBar1 != null && healthBar2 != null)
        {
            if (speed != 0)
            {
                healthBar1.fillAmount = HP / maxHP;
                healthBar2.fillAmount = HP / maxHP;
                enemyHealthLabel.text = HP.ToString();
            }
        }
    }

    IEnumerator Boss()
    {
        isBossing = true;
        animator.SetBool("Attack", false);
        animator.SetBool("Summon", false);
        yield return new WaitForSeconds(3);
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(3);
        animator.SetBool("Summon", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Summon", false); ;
        isBossing = false;
        StartCoroutine(Boss());
    }

    public void ShootGun()
    {
        if (gunone != null)
        {
            gunone.Play();
        }
        if (guntwo != null)
        {
            guntwo.Play();
        }
    }

    public void SummonEnemies()
    {
        Instantiate(enemy, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemy, spawnPoint2.transform.position, Quaternion.identity);

    }

    private void UseGun()
    {
        if (gun1 != null)
        {
            gun1.transform.LookAt(target);
        }
        if (gun2 != null)
        {
            gun2.transform.LookAt(target);
        }
    }

    private void UpdateHealthBar()
    {
        if (isBossFight)
        {
            if (HealthLabel != null)
            {
                HealthLabel.text = HP.ToString();
            }
            if (HealthBar != null)
            {
                HealthBar.fillAmount = HP / maxHP;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (canBeDamaged)
        {
            if (other.tag == "Bullets")
            {
                //Destroy(collision.gameObject);
                HP -= other.transform.parent.gameObject.GetComponent<GunScript>().damage;
                hurtSfx.Play();
                if (HP <= 0)
                {
                    if (currencyHolder != null)
                    {
                        currencyHolder.IncreaseGold(goldReward);
                    }
                    if (isExplosive) { StartCoroutine(Explosion()); return; }
                    else
                    {
                        currencyHolder.EnemyKilled();
                        audioSource.PlayOneShot(deathSoundEffect, 1f);
                        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
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
            speed = 0f;
            explosionFlash.SetActive(true);
            yield return new WaitForSeconds(.2f);
            explosionFlash.SetActive(false);
            yield return new WaitForSeconds(.2f);
            explosionFlash.SetActive(true);
            yield return new WaitForSeconds(.2f);
            explosionFlash.SetActive(false);
            yield return new WaitForSeconds(.2f);
            deathParticles.Play();
            audioSource.PlayOneShot(deathSoundEffect, 1f);
            Destroy(sprite);
            if (Vector2.Distance(target.position, transform.position) < 4f)
            {
                target.gameObject.GetComponent<PlayerHealth>().TakeDamage(50);
            }
            yield return new WaitForSeconds(0.1f); currencyHolder.EnemyKilled();
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
        if (Vector2.Distance(target.position, transform.position) < stoppingDistance) { rb.velocity = new Vector2(0, 0); StartCoroutine(AttackPlayer()); return; }
        Vector3 direction = (target.position - transform.position).normalized;
        //float angle = Mathf Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;

        sprite.transform.localScale = new Vector2(Mathf.Sign(direction.x) * -2.5f, 2.5f);

        moveDirection = direction;
        if (slippery)
        {
            rb.velocity += new Vector2(moveDirection.x, moveDirection.y) * speed * Time.deltaTime;

        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
    }

    IEnumerator AttackPlayer()
    {
        if (isExplosive) { StartCoroutine(Explosion()); yield return new WaitForSeconds(0); }
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

