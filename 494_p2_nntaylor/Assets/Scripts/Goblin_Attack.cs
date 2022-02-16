using System;
using UnityEngine;

public class Goblin_Attack : MonoBehaviour
{
    public float fireRate = 2.0f;
    private GameObject _projectile;
    private SpriteRenderer _spr;

    private void Awake()
    {
        _projectile = Resources.Load<GameObject>("PreFabs/Projectile");
        _spr = GetComponent<SpriteRenderer>();
    }

    private float _t = 0.0f;
    private void Update()
    {
        if (_spr.isVisible)
        {
            _t += Time.deltaTime;
            if (_t >= fireRate)
            {
                print("fire!");
                FireProjectiles();
                _t = 0.0f;
            }
        }
    }

    private void FireProjectiles()
    {
        Vector2 pos = transform.position;
        GameObject[] gos = new GameObject[4];
        
        for (int i = 0; i < gos.Length; ++i)
            gos[i] = Instantiate(_projectile);
        
        gos[0].transform.position = new Vector2(pos.x, pos.y + 1);
        Rigidbody2D rb2d = gos[0].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.up * 5.0f;
        
        gos[1].transform.position = new Vector2(pos.x, pos.y - 1);
        rb2d = gos[1].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * 5.0f;
        
        gos[2].transform.position = new Vector2(pos.x + 1, pos.y);
        rb2d = gos[2].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * 5.0f;
        
        gos[3].transform.position = new Vector2(pos.x - 1, pos.y);
        rb2d = gos[3].GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left * 5.0f;
    }
}
