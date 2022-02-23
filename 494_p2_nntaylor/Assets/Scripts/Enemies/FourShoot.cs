using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourShoot : MonoBehaviour
{
    public float fireTime = 2.0f;
    private float _time = 0f;
    private Color _thisColor;
    private GameObject _projectile;

    private void Awake()
    {
        _thisColor = gameObject.GetComponent<SpriteRenderer>().color;
        // Assets/Resources/PreFabs/Projectile.prefab
        _projectile = Resources.Load<GameObject>("PreFabs/Projectile");
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= fireTime)
        {
            _time = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 pos = transform.position;
        GameObject[] gos = new GameObject[4];

        for (int i = 0; i < gos.Length; ++i)
        {
            gos[i] = Instantiate(_projectile);
            gos[i].GetComponent<SpriteRenderer>().color = _thisColor;
        }

        gos[0].transform.position = new Vector2(pos.x, pos.y + 1.1f);
        Rigidbody2D rb2d = gos[0].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.up * 5.0f;
        
        gos[1].transform.position = new Vector2(pos.x, pos.y - 1.1f);
        rb2d = gos[1].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * 5.0f;
        
        gos[2].transform.position = new Vector2(pos.x + 1.1f, pos.y);
        rb2d = gos[2].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * 5.0f;
        
        gos[3].transform.position = new Vector2(pos.x - 1.1f, pos.y);
        rb2d = gos[3].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left * 5.0f;
    }
}
