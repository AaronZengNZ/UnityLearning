using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] GameObject myGameObject;
    [SerializeField] new Vector2 respawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            myGameObject.transform.position = respawnLocation;
            myGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        if (collision.CompareTag("BadObject"))
        {
            myGameObject.transform.position = respawnLocation;
            myGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
