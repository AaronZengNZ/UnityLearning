using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float gravity = 5f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] BoxCollider myCollider;
    bool touchingGround;


    // Start is called before the first frame update
    void Start()
    {
        PrintInstructions();
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RunJump();
    }

 
    private void OnCollisionEnter(Collision other)
    {
        touchingGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        touchingGround = false;
    }

    void RunJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
            
            while (!Input.GetKeyUp(KeyCode.Space))
            {
                if (touchingGround == false) { return; }


                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.y);
            }

        }
    }

    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        if (zValue > Time.deltaTime/2*moveSpeed || zValue < Time.deltaTime/2*-moveSpeed)
        {
            GetComponent<Rigidbody>().velocity += new Vector3(xValue*4, 0, zValue);
            if (GetComponent<Rigidbody>().velocity.z < 50)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, 0, zValue * 6);
            }
        }
        else
        {
            GetComponent<Rigidbody>().velocity -= new Vector3(xValue * 30, 0, zValue * 30);
        }
    }



    //to delete: everything
    void PrintInstructions()
    {
        //eggs
        Debug.Log("Haha why are you playing this bad game");
        Debug.Log("Stop playing, right now");
        Debug.Log("You have better things to do, leave now >:");
    }

}
