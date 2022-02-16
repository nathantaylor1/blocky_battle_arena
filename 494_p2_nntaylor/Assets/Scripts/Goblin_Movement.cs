using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goblin_Movement : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D _rb2d;
    private LayerMask _wall;
    private Collider2D _c2d;

    private void Start()
    {
        _wall = LayerMask.NameToLayer("Wall");
        _rb2d = GetComponent<Rigidbody2D>();
        _c2d = GetComponent<Collider2D>();
        NewDirection();
    }

    private void Update()
    {
        RaycastHit2D rch2d = Physics2D.Raycast(_c2d.bounds.center, direction, 
            _c2d.bounds.extents.x + 0.25f);
        if (rch2d.transform != null && 
            (rch2d.transform.CompareTag("Wall") || rch2d.transform.CompareTag("Player")))
            NewDirection();
        if (Mathf.Abs(_rb2d.velocity.magnitude) <= 0.2f)
            NewDirection();
    }

    private void NewDirection()
    {
        int rouletteDirection = Random.Range(1, 5);
        
        if (rouletteDirection == 1)
            direction = Vector2.up;
        else if (rouletteDirection == 2)
            direction = Vector2.right;
        else if (rouletteDirection == 3)
            direction = Vector2.down;
        else
            direction = Vector2.left;

        _rb2d.velocity = direction * 2.0f;
    }
}
