using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_controller;
    private Vector3 move_direction;
    public float speed = 5f;
    private float gravity = 20f;
    public float jump_force = 5f;
    private float vertical_velocity;

    private bool jumped = false; //added to make water (and hopefully ZeroG) work

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); //get left and right from keyboard 

        move_direction = transform.TransformDirection(move_direction); //transform from local space to world space
        move_direction = move_direction * speed * Time.deltaTime; //multiply move direction by speed by frames per second

        ApplyGravity(); //check if we jumped

        character_controller.Move(move_direction); //move the character
    }

    void ApplyGravity()
    {
        if (character_controller.isGrounded)
        {
            vertical_velocity = -gravity * Time.deltaTime; //clear gravity from vertical velocity 

            jumped = false;

            PlayerJump(); //check if we jump

        }

        else
        {
            if (jumped) 
                vertical_velocity -= gravity * Time.deltaTime;
            else
                vertical_velocity = -gravity*30.5f * Time.deltaTime;
        }

        move_direction.y = vertical_velocity * Time.deltaTime; //translate in the y direction

    }

    void PlayerJump()
    {
        if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        { //check if we pressed space bar
            vertical_velocity += jump_force*1.5f;

            jumped = true;
        }
    }

}
