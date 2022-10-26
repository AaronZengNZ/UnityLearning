using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    bool canShoot = true;

    public GameObject[] guns;
    public ParticleSystem[] gunShots;

    public GameObject sprite;
    public float fps;
    public TextMeshProUGUI fpsText;

    public float focusTimeMs = 500f;
    public float focusMs = 0;

    public float shootingSlowdown = 5f;

    float tempFps;

    Gun currentGun;

    public int gun = 0;
    float fpsLoad = 0;

    float bursts = 1;
    float bTime = 0.2f;

    public GameObject temp;
    public GameObject temp2;
    public GameObject ai;
    // Start is called before the first frame update
    public int NameToLocation(string name)
    {
        //loop through all the guns in the guns array and find one with the same name as name
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].GetComponent<Gun>().name == name)
            {
                //return the location of the gun
                return i;
            }
        }
        return 99999;
    }
    public void EquipGun(string name)
    {
        if (NameToLocation(name) != 99999)
        {
            gun = NameToLocation(name);
            //set burst to the gun's burst
            bursts = guns[gun].GetComponent<Gun>().burst;
            //set btime to the gun's time
            bTime = guns[gun].GetComponent<Gun>().time;
        }
    }
    private void SetPfxs()
    {
        //set gunshots to the gun's gunshoot
        for (int i = 0; i < gunShots.Length; i++)
        {
            gunShots[i] = guns[i].GetComponent<Gun>().gunShoot;
        }
    }

    void Start()
    {
        SetPfxs();
        StartCoroutine(Move());
        StartCoroutine(Upd10());
    }
    IEnumerator Upd10()
    {
        while (true)
        {
            //disable all guns and activate the current one
            //if gun is more than guns.length, set it to guns.length
            if (gun > guns.Length - 1)
            {
                gun = guns.Length - 1;
            }
            for (int i = 0; i < guns.Length; i++)
            {
                if (i != gun)
                {
                    guns[i].SetActive(false);
                }
            }
            //set gun to currentgun
            currentGun = guns[gun].GetComponent<Gun>();
            guns[gun].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            focusMs += 100;
            fps += tempFps;
            tempFps = 0;
            fpsLoad += 1;
            if (fpsLoad == 10)
            {
                fpsText.text = fps.ToString() + " fps";
                fps = 0;
                fpsLoad = 0;
            }
        }
    }
    //shoot coroutine
    IEnumerator Shoot()
    {
        //point towards mouse pointer

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        GetComponent<Rigidbody2D>().AddForce(-sprite.transform.up * currentGun.recoil * 10);
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        //set bursts to the gun's bursts
        bursts = guns[gun].GetComponent<Gun>().burst;
        //set btime to the gun's btime
        bTime = guns[gun].GetComponent<Gun>().time;

        if (bursts > 1)
        {
            for (int i = 1; i < bursts; i++)
            {
                yield return new WaitForSeconds(bTime);
                if (currentGun.triShot)
                {
                    StartCoroutine(tri());
                }
                else
                {
                    gunShots[gun].Play();
                }
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                sprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
                GetComponent<Rigidbody2D>().AddForce(-sprite.transform.up * currentGun.recoil * 10);
                sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        //if the gun has a holdpoint
        if (currentGun.holdPoint)
        {
            //rotate the holdpoint 360 degrees over the time of the gun's shootspeed
            yield return new WaitForSeconds(1 / currentGun.shootSpeed / 54 * 4);
            for (float i = 0; i < 36; i++)
            {
                //turn the gun's holdpoint right by 10 degrees
                if (currentGun.holdPoint)
                {
                    currentGun.holdPoint.transform.Rotate(0, 0, 10);
                }
                yield return new WaitForSeconds(1 / currentGun.shootSpeed / 54);
            }
            yield return new WaitForSeconds(1 / currentGun.shootSpeed / 54 * 14);
        }
        else
        {
            yield return new WaitForSeconds(1 / currentGun.shootSpeed);
        }
        canShoot = true;
    }

    IEnumerator tri()
    {
        //play the gunShoot2 of gun
        guns[gun].GetComponent<Gun>().gunShoot.Play();
        yield return new WaitForSeconds(1 / currentGun.shootSpeed / 54 * 1.5f);
        guns[gun].GetComponent<Gun>().gunShoot2.Play();
        yield return new WaitForSeconds(1 / currentGun.shootSpeed / 54 * 1.5f);
        guns[gun].GetComponent<Gun>().gunShoot3.Play();
    }
    // make a movement function corresponding to a fixed update
    void Update()
    {
        //make temp2 point towards ai
        temp2.transform.rotation = Quaternion.LookRotation(Vector3.forward, ai.transform.position - temp2.transform.position);
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (canShoot)
        {
            if (currentGun.holdPoint)
            {
                currentGun.holdPoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            }
        }
        //make gun point at mouse
        tempFps += 1;
        temp.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        guns[gun].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetMouseButton(0))
        {
            if (canShoot && focusMs > focusTimeMs)
            {
                //if the gun is trishot
                if (currentGun.triShot)
                {
                    //startcoroutine tri
                    StartCoroutine(tri());
                }
                else
                {
                    gunShots[gun].Play();
                }
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }
        //if the mouse is to the left of the player, flip the sprite
        if (mousePos.x < transform.position.x)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }
        //if the mouse is to the left of the player, reverse the y scale of gun
        if (mousePos.x < transform.position.x)
        {
            guns[gun].transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            guns[gun].transform.localScale = new Vector3(1, 1, 1);
        }
    }
    Rigidbody2D rb;
    IEnumerator Move()
    {
        while (true)
        {
            //make a vector2 called movement and get the input
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //if there's any movement at all
            if (movement != Vector2.zero)
            {
                focusMs = 0f;
            }
            rb = GetComponent<Rigidbody2D>();
            rb.velocity += movement * speed * 0.05f;

            Vector3 vel = rb.velocity;

            //if rigidbody is too fast make it movement*speed
            if (GetComponent<Rigidbody2D>().velocity.x > speed)
            {
                vel.x = movement.x * speed;
                rb.velocity = vel;
            }
            if (GetComponent<Rigidbody2D>().velocity.y > speed)
            {
                vel.y = movement.y * speed;
                rb.velocity = vel;
            }
            yield return new WaitForSeconds(0.05f);
            //if mouse is held down, continue waiting until mouse is released using a while loop
            while (Input.GetMouseButton(0))
            {
                yield return new WaitForSeconds(0.05f);
                movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                rb = GetComponent<Rigidbody2D>();
                rb.velocity += movement * speed * 0.05f / shootingSlowdown;
                vel = rb.velocity;
                if (GetComponent<Rigidbody2D>().velocity.x > speed)
                {
                    vel.x = movement.x * speed;
                    rb.velocity = vel;
                }
                if (GetComponent<Rigidbody2D>().velocity.y > speed)
                {
                    vel.y = movement.y * speed;
                    rb.velocity = vel;
                }
            }
        }
    }
}