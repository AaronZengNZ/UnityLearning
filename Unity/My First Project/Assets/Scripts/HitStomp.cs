using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStomp : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2D;
    [SerializeField] AudioClip enemyHurtSfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Patrol>().DecreaseHp(1f);
            bool enemyHp = other.gameObject.GetComponent<Patrol>().ReturnHp();
            AudioSource.PlayClipAtPoint(enemyHurtSfx, Camera.main.transform.position);
            if (enemyHp)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, bounce / 1.5f);
            }
            else { 
                Destroy(other.gameObject);
                rb2D.velocity = new Vector2(rb2D.velocity.x, bounce);
            }
        }
        else
        {
            if (other.CompareTag("Chest"))
            {
                Destroy(other.gameObject);
                rb2D.velocity = new Vector2(rb2D.velocity.x, 6f);
            }
        }
    }
}
