using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] bool isConfined = false;
    [SerializeField] float hp = 3f;
    public GameObject coin;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isConfined)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isConfined)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }
    }

    public bool ReturnHp()
    {
        if(hp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DecreaseHp(float amount)
    {
        hp -= amount;
    }

    private void OnDestroy()
    {
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
    }
}
