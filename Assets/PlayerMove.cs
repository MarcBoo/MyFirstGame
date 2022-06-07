using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Cast controller onto Player GO
    public CharacterController controller;

    //Movement speed
    public float speed = 12f;

    //Physic gravity used on player
    public float gravity = -9.81f;

    //force to be jumped regarding y axis
    public float jumpHeight = 3f;

    //Series of variables to check for ground collission
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform playerBody;

    Vector3 velocity;


    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Soft landing
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //Input of forward and sideways keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Check for jump and then jump accordingly
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //create movement vector
        Vector3 move = transform.right * x + transform.forward * z;
        //do move on chosen GO
        controller.Move(move * speed * Time.deltaTime);

        //Add gravity to vertical velocity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Execute damage on oneself after lMBC; for testing purposes
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Click");
            DealDamage DD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DealDamage>();
            DD.SendDamage(10);
        }
    }
}
