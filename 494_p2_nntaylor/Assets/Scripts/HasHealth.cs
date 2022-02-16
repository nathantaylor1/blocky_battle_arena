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
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Sword"))
        {
            TakeDamage(5f);
        }
    }

    private bool _changingRunning = false;
    private IEnumerator ChangeColorFromHit()
    {
        if (!_changingRunning)
        {
            _changingRunning = true;
            Color c = _spr.color;
            _spr.color = (Color.red);
            yield return new WaitForSeconds(0.2f);
            _spr.color = c;
            yield return new WaitForSeconds(0.1f);
            _changingRunning = false;
        }
        
        yield return null;
    }
}
