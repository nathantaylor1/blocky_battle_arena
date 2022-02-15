using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_BreakBlock : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Collider2D _collider2D;
    private float _maxRaycastDist;

    public LayerMask wallLayer;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _maxRaycastDist = _collider2D.bounds.extents.x + 0.2f;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            _currentDirection = Player_MovementController.instance.GetDirection();
            RaycastHit2D hit = Physics2D.Raycast(_collider2D.bounds.center, 
                _currentDirection, _maxRaycastDist, wallLayer);
            if (hit.transform != null && hit.transform.CompareTag("Wall"))
            {
                Breakable breakable = hit.transform.GetComponent<Breakable>();
                breakable.BreakThisBlock();
            }
        }
        
    }
}
