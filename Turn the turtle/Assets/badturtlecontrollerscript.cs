using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badturtlecontrollerscript : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpHeight = 5f;

    // State
    // Audio

    // Cached content references
    Rigidbody2D myRigidBody;
    PolygonCollider2D myBodyCollider2D;
    float gravityScaleAtStart;


    //Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider2D = GetComponent<PolygonCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * movementSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool isRunning = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private void Jump()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpHeight);
            if (myRigidBody.velocity.y < 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
            }
            myRigidBody.velocity += jumpVelocityToAdd;

        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f); 
        }
    }
}
