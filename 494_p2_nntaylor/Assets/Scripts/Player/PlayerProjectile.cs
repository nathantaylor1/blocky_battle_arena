using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Enemy"))
            obj.GetComponent<HasHealth>().TakeDamage(3f);
        else if (obj.CompareTag("Wall"))
        {
            Breakable bb = obj.GetComponent<Breakable>();
            if (bb != null)
                bb.BreakThisBlock();
        }
    }
}
