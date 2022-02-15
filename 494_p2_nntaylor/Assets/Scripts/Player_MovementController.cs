using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MovementController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
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
        _rigidbody2D.velocity = movement * movementSpeed;
    }
    
    private void FixedUpdate()
    {
        UpdateVelocity();
    }
}
