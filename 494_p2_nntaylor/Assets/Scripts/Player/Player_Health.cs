using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public bool invincible = false;
    public static Player_Health instance;
    public float health = 10f;
    public Text healhText;
    private SpriteRenderer _spr;
    public GameObject GameOverScreen;

    private void Awake()
    {
        instance = this;
        _spr = GetComponentInChildren<SpriteRenderer>();
        UpdateHPText();
    }
    
    public void TakeDamage(float damage)
    {
        if (invincible) return;
        health -= damage;
        UpdateHPText();
        StartCoroutine(ChangeColorFromHit());
        if (health <= 0)
        {
            Time.timeScale = 0f;
            GameOverScreen.SetActive(true);
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

    public void UpdateHPText()
    {
        healhText.text = health.ToString("00");
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HealthUp"))
        {
            health += col.GetComponent<Item>().worth;
            UpdateHPText();
            Destroy(col.gameObject);
        }
    }
}
