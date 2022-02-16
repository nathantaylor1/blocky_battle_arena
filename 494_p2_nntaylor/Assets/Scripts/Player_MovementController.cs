using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MovementController : MonoBehaviour
{
    public static Player_MovementController instance;
    public float movementSpeed = 2.0f;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 currentDirection;
    private Vector2 lastDirection;

    private void Awake()
    {
        instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0.0f;
    }

    private void UpdateVelocity()
    {
        // Get Inputs:
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Update Rigidbody2D Velocity:
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        currentDirection = movement;
        if (currentDirection.magnitude != 0f && !(horizontalInput != 0 && verticalInput != 0))
        {
            lastDirection = currentDirection;
        }
        _rigidbody2D.velocity = movement * movementSpeed;
    }
    
    private void FixedUpdate()
    {
        UpdateVelocity();
    }

    public Vector2 GetDirection()
    {
        return currentDirection;
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}
