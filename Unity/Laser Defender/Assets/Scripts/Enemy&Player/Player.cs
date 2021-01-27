using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] int shield = 200;

    [Header("VFX and SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.6f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.25f;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.4f;
    [SerializeField] AudioClip shootBombSFX;
    [SerializeField] [Range(0, 1)] float shootBombSFXVolume = 0.45f;
    [SerializeField] AudioClip shieldDestroySFX;
    [SerializeField] [Range(0, 1)] float shieldDestroySFXVolume = 0.4f;

    [Header("Projectiles")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.08f;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float bombSpeed = 6f;
    [SerializeField] float bombFiringPeriod = 3f;

    Coroutine firingCoroutine;
    Coroutine bombingCoroutine;

    float xMin;
    float yMin;
    float xMax;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        if (damageDealer.GetDamage() < 0)
        {
            shield -= damageDealer.GetDamage();
            damageDealer.Hit();
            return;
        }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (shield > 0)
        {
            shield -= damageDealer.GetDamage();
            if (shield <= 0)
            {
                shield = 0;
                AudioSource.PlayClipAtPoint(shieldDestroySFX, Camera.main.transform.position, shieldDestroySFXVolume);
            }
        }
        else
        {
            health -= damageDealer.GetDamage();
        }
        damageDealer.Hit();
        
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetShield()
    {
        return shield;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            bombingCoroutine = StartCoroutine(FireBombsContinuosly());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(bombingCoroutine);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate
                    (laserPrefab,
                    transform.position,
                    Quaternion.identity)
                    as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    IEnumerator FireBombsContinuosly()
    {
        while (true)
        {
            GameObject bomb = Instantiate
                    (bombPrefab,
                    transform.position,
                    Quaternion.identity)
                    as GameObject;
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bombSpeed);
            AudioSource.PlayClipAtPoint(shootBombSFX, Camera.main.transform.position, shootBombSFXVolume);
            yield return new WaitForSeconds(bombFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, transform.position.y);
        transform.position = new Vector2(transform.position.x, newYPos);
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

}
