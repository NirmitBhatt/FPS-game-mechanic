using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const string Horizontal = "Horizontal";
    const string Vertical = "Vertical";
    const string Jump = "Jump";

    [SerializeField]
    private float playerMovementSpeed = 12f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private float groundDistance = 0.4f;

    private float movementAlongXAxis;
    private float movementAlongZAxis;
    private bool isGrounded;
    private Vector3 velocity;

    public CharacterController characterController; 
    public Transform groundCheck;
    public LayerMask groundLayerMask;
    
    void Update()
    {
        MovePlayerWASD();
        ApplyFallGravityForPlayer();

        if (Input.GetButtonDown(Jump) && isGrounded)
        {
            PlayerJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            PlayerCrouch();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ResetCharacterControllerHeight();
        }
    }

    private void PlayerCrouch()
    {
        characterController.height = (characterController.height/2);
        playerMovementSpeed = 4.5f;      
    }

    private void ResetCharacterControllerHeight()
    {
        characterController.height = 3.8f;
        playerMovementSpeed = 12f;
    }

    private void ApplyFallGravityForPlayer()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        CheckIfPlayerIsOnGround();
    }

    private void PlayerJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void CheckIfPlayerIsOnGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (isGrounded && velocity.y < 0)
        {
            ResetVelocity();
        }
    }

    private void ResetVelocity()
    {
        velocity.y = -2f;
    }

    private void MovePlayerWASD()
    {
        movementAlongXAxis = Input.GetAxis(Horizontal);
        movementAlongZAxis = Input.GetAxis(Vertical);
        Vector3 move = transform.right * movementAlongXAxis + transform.forward * movementAlongZAxis;
        characterController.Move(move * playerMovementSpeed * Time.deltaTime);
    }
}
