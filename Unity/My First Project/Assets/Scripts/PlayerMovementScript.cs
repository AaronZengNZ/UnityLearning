using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float playerSize = 0.5f;
    [SerializeField] float jumpDelay = 0.1f;
    bool isJumping = false;
    bool isTouchingGround = false;
    bool canTouchGround = true;

    Rigidbody2D myRigidBody;
    BoxCollider2D myCollider;
    GameObject myGameObject;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }




    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        

        bool isRunning = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private void Jump()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (!isTouchingGround)
            {
                if (canTouchGround)
                {
                    isTouchingGround = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || isJumping)
        {
            Debug.Log("jumping");
            if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) & isTouchingGround == false) { if (!isJumping) { StartCoroutine(StillJumpingDelay()); } return; }
            else {
                isTouchingGround = false;

                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpHeight);
            }
            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                StartCoroutine(NoLongerJumping());
            }
        }
    }
    
    IEnumerator NoLongerJumping()
    {
        yield return new WaitForSeconds(0.001f);
        isJumping = false;
        isTouchingGround = false;
        canTouchGround = false;
        yield return new WaitForSeconds(jumpDelay);
        canTouchGround = true;
    }

    IEnumerator StillJumpingDelay()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpDelay);
        isJumping = false;
    }


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * playerSize, playerSize);
        }
    }


}
