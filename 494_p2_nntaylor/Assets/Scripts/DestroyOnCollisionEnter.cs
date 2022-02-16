using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionEnter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Player_Health php = col.transform.GetComponent<Player_Health>();
            php.TakeDamage(2f);
        }
        Destroy(gameObject);
    }
}
