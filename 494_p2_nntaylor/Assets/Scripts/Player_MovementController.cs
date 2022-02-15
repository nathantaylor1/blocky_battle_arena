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
}
