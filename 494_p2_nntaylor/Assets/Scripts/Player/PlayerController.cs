using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float speed = 3.0f;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite1, _sprite2;
    private Vector3 _dir;
    private GameObject _projectilesPrefab;
    public float shootCooldownTime = 0.35f;

    private void Awake()
    {
        instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        // Assets/Resources/Sprites/1ShooterSprite.png
        _sprite1 = Resources.Load<Sprite>("Sprites/1ShooterSprite");
        
        // Assets/Resources/Sprites/1Shooter90Sprite.png
        _sprite2 = Resources.Load<Sprite>("Sprites/1Shooter90Sprite");
        
        // Assets/Resources/PreFabs/PlayerProjectile.prefab
        _projectilesPrefab = Resources.Load<GameObject>("PreFabs/PlayerProjectile");
    }

    private void UpdatePlayerVelocity()
    {
        Vector3 vel = _rb.velocity;
        vel.x = MoveHorizontalAxis;
        vel.y = MoveVerticalAxis;
        _rb.velocity = vel.normalized * speed;

        if (vel == _dir || vel.magnitude <= 0.5f) return;
        _dir = vel;
        if (_dir == Vector3.up)
        {
            _spriteRenderer.sprite = _sprite1;
            _spriteRenderer.flipY = false;
        }
        else if (_dir == Vector3.down)
        {
            _spriteRenderer.sprite = _sprite1;
            _spriteRenderer.flipY = true;
        }
        else if (_dir == Vector3.right)
        {
            _spriteRenderer.sprite = _sprite2;
            _spriteRenderer.flipX = false;
        }
        else if (_dir == Vector3.left)
        {
            _spriteRenderer.sprite = _sprite2;
            _spriteRenderer.flipX = true;
        }
    }

    private bool _canShoot = true;
    private void PlayerShoot()
    {
        if (!Input.GetKeyDown(KeyCode.X) || !_canShoot) return;

        StartCoroutine(CO_ShootCooldown());
        Vector3 pos = transform.position;
        GameObject go = Instantiate(_projectilesPrefab);
        go.GetComponent<Rigidbody2D>().velocity = _dir * 5.0f;
        
        if (_dir == Vector3.up)
        {
            go.transform.position = new Vector3(pos.x, pos.y + 0.8f, 0.0f);
        }
        else if (_dir == Vector3.down)
        {
            go.transform.position = new Vector3(pos.x, pos.y - 0.8f, 0.0f);
        }
        else if (_dir == Vector3.right)
        {
            go.transform.position = new Vector3(pos.x + 0.8f, pos.y, 0.0f);
        }
        else /*(_dir == Vector3.left)*/
        {
            go.transform.position = new Vector3(pos.x - 0.8f, pos.y, 0.0f);
        }
    }

    private IEnumerator CO_ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootCooldownTime);
        _canShoot = true;
        yield return null;
    }

    private void Update()
    {
        UpdateMoveInput();
        UpdatePlayerVelocity();
        PlayerShoot();
    }

    public void SlowPlayer(float slowTime = 0.5f)
    {
        StartCoroutine(CO_SlowEffect(slowTime));
    }

    private IEnumerator CO_SlowEffect(float slowTime)
    {
        speed /= 2.0f;
        yield return new WaitForSeconds(slowTime);
        speed *= 2.0f;
        yield return null;
    }

    private float MoveHorizontalAxis, MoveVerticalAxis;
    public void UpdateMoveInput()
    {
        MoveHorizontalAxis = (Input.GetKey(KeyCode.RightArrow)?1f:0f) - (Input.GetKey(KeyCode.LeftArrow)?1f:0f);
        MoveVerticalAxis   = (Input.GetKey(KeyCode.UpArrow)?1f:0f) - (Input.GetKey(KeyCode.DownArrow)?1f:0f);
    }
}
