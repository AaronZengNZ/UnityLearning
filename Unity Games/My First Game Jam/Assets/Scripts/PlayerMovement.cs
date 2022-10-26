using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] float padding = 1;
    [SerializeField] ParticleSystem staff;
    [SerializeField] GameObject sprite;
    [SerializeField] bool isTutorial = false;
    [SerializeField] Tutorial tutorial;
    [SerializeField] float shootSpeed = 3f;
    [SerializeField] bool autoFire = false;
    [SerializeField] GunScript gunScript;

    [SerializeField] AudioSource shootSfx;
    bool hasMoved = false;
    bool hasShot = false;
    bool canShoot = true;

    float xMin;
    float yMin;
    float xMax;
    float yMax;

    [SerializeField] CurrencyHolder currencyHolder;
    // Start is called before the first frame update
    void Start()
    {
        currencyHolder = FindObjectOfType<CurrencyHolder>();
        gunScript = FindObjectOfType<GunScript>();
        SetUpMoveBoundaries();
        if(currencyHolder.GetTwist() != null)
        {
            string twist = currencyHolder.GetTwist();
            ParticleSystem.MainModule psMain = staff.main;
            var psCollision = staff.collision;
            if (twist == "hp")
            {
                GetComponent<PlayerHealth>().SetHealth(200);
            }
            if(twist == "dampenBounce") { 
                psCollision.bounce = 0.85f;
            }
            if (twist == "gravity")
            {
                psMain.gravityModifier = 0.7f;
            }
        }
        if (currencyHolder.GetTwist(2) != null)
        {
            string twist = currencyHolder.GetTwist(2);
            ParticleSystem.MainModule psMain = staff.main;
            if (twist == "fast")
            {
                shootSpeed = 6;
                autoFire = true;
                gunScript.damage = 10;
                psMain.startSize = 0.4f;
                GetComponent<PlayerHealth>().SetSelfDamage(5);
            }
            if (twist == "slow")
            {
                shootSpeed = 1f;
                gunScript.damage = 80;
                psMain.startSize = 1;
                GetComponent<PlayerHealth>().SetSelfDamage(25);
            }
            if (twist == "shield")
            {
                GetComponent<PlayerHealth>().SetSelfDamage(2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void Shoot()
    {
        if (autoFire && Input.GetMouseButton(0))
        {
            CalculateShoot();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CalculateShoot();
        }
    }

    private void CalculateShoot()
    {
        if (canShoot == true)
        {
            StartCoroutine(shootCooldown());
            if (isTutorial == false)
            {
                staff.Play();
                shootSfx.Play();
            }
            else if (hasMoved == true && hasShot == false && isTutorial == true)
            {
                staff.Play();
                shootSfx.Play();
                tutorial.SetTextActive(2);
                hasShot = true;
            }
            else if (hasShot == true && isTutorial == true)
            {
                staff.Play();
                shootSfx.Play();
            }
            
        }
    }

    IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / shootSpeed);
        Debug.Log("cooldowned");
        canShoot = true;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        if (Mathf.Round(deltaX * 100) != 0)
        {
            sprite.transform.localScale = new Vector2(Mathf.Sign(deltaX) * 2f, 2f);
            if(hasMoved == false && isTutorial == true)
            {
                tutorial.SetTextActive(1);
                hasMoved = true;
            }
        }
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
