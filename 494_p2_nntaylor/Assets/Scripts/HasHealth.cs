using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HasHealth : MonoBehaviour
{
    public float health = 10f;
    private SpriteRenderer _spr;

    private GameObject _copper, _silver, _gold;

    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _copper = Resources.Load<GameObject>("PreFabs/Collectables/CopperCoin");
        _silver = Resources.Load<GameObject>("PreFabs/Collectables/SilverCoin");
        _gold = Resources.Load<GameObject>("PreFabs/Collectables/GoldCoin");
    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        StartCoroutine(ChangeColorFromHit());
        if (health <= 0)
        {
            DropLoot();
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
    private void DropLoot()
    {
        int randNum = Random.Range(0, 100);
        GameObject go;
        if (randNum <= 5f)
        {
            // TODO: Gold Coin
            go = Instantiate(_gold);
        }
        else if (randNum <= 25f)
        {
            // TODO: Silver Coin
            go = Instantiate(_silver);
        }
        else
        {
            // TODO: Copper Coin
            go = Instantiate(_copper);
        }

        go.transform.position = transform.position;
    }
}
