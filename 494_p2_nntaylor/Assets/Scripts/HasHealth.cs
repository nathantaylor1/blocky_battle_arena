using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        if (health <= 0f)
        {
            
            if (gameObject.CompareTag("Player"))
            {
                // TODO:
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
            }
            
        }
        
    }
}
