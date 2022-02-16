using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public float health = 10f;
    private SpriteRenderer _spr;

    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        StartCoroutine(ChangeColorFromHit());

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Sword") && gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5f);
        }
    }

    private IEnumerator ChangeColorFromHit()
    {
        Color c = _spr.color;
        _spr.color = (Color.red);
        yield return new WaitForSeconds(0.2f);
        _spr.color = c;
        
        if (health <= 0f && gameObject.CompareTag("Player"))
        {
            // TODO:
        }
        else if (health <= 0f && gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        
        yield return null;
    }
}
