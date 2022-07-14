using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool forwardPressed = Input.GetAxis("Vertical") > 0;
        bool backwardPressed = Input.GetAxis("Vertical") < 0;

        bool rightPressed = Input.GetAxis("Horizontal") > 0;
        bool leftPressed = Input.GetAxis("Horizontal") < 0;

        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);

        //* Forwards and backwards movement
        if (forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }
        else if(backwardPressed)
        {
            animator.SetBool("isWalkingBack", true);
        }
        else 
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBack", false);
        }

        //* left and right movement
        if (rightPressed) 
        {
            animator.SetBool("isWalkingRight", true);
        }
        else if (leftPressed)
        {
            animator.SetBool("isWalkingLeft", true);
        }
        else
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }

        //* Jumping
        if (!playerMovement.isGrounded) 
        {
            animator.SetBool("isJumping", true);
        }

        if(playerMovement.isGrounded) 
        {
            animator.SetBool("isJumping", false);
        }

        //* Sprinting
        if (sprintPressed) 
        {
            animator.SetBool("isSprinting", true);
        }
        else 
        {
            animator.SetBool("isSprinting", false);
        }
    }
}
