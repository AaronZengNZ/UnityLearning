using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public float gravity = -5;
    public float multiplier = 1;
    float velocityY = 0;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = Camera.main.transform.rotation;
        velocityY += gravity * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        input = input.normalized;

        Vector3 temp = Vector3.zero;
        temp += transform.forward * input.z;
        temp += transform.right * input.x;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            temp += transform.forward * input.z / 2;
            temp += transform.right * input.x / 2;
        }

        Vector3 velocity = temp * speed;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY = 15;
            }
        }
    }
}

