using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float maxHp = 100f;
    public float hp = 100f;
    public Image leftHpBar;
    public Image rightHpBar;

    public float baseBar = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //set rb to rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }

    //on partilce collision, take damage equal to the other's parent's parent's gun's damage
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Gun")
        {
            Gun gun;
            //set gun to the collision's parents parent
            if(other.gameObject.transform.parent.parent.parent.GetComponent<Gun>()){
                gun = other.gameObject.transform.parent.parent.parent.GetComponent<Gun>();
            }
            else{
                gun = other.gameObject.transform.parent.parent.GetComponent<Gun>();
            } 
            //take damage
            hp -= gun.damage;
            //if hp is less than 0, destroy the enemy
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move left
        rb.velocity += new Vector2(-speed * Time.deltaTime, 0f);
        //if too fast, set to max speed
        if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //set hp bars
        leftHpBar.fillAmount = (hp / maxHp * (baseBar / -100 + 1)) + (baseBar / 100);
        rightHpBar.fillAmount = (hp / maxHp * (baseBar / -100 + 1)) + (baseBar / 100);
    }
}
