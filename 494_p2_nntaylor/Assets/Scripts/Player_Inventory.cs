using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public static Player_Inventory instance;
    private bool _hasPickaxe = false, _hasSword = false;

    public bool HasPickaxe() { return _hasPickaxe; }
    public bool HasSword() { return _hasSword; }

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PowerUp"))
        {
            PowerUp pup = col.gameObject.GetComponent<PowerUp>();

            if (pup.thisPowerUp == Items.PickaxePowerUp)
            {
                Player_BreakBlock pbb = GetComponent<Player_BreakBlock>();
                if (!_hasPickaxe)
                {
                    _hasPickaxe = true;
                    pbb.enabled = true;
                    pbb.AddNumBlocksCanBreak(30);
                    col.gameObject.SetActive(false);
                }
                else
                {
                    pbb.AddNumBlocksCanBreak(5);
                    col.gameObject.SetActive(false);
                }
            }
            else if (pup.thisPowerUp == Items.SwordPowerUp)
            {
                _hasSword = true;
                col.gameObject.SetActive(false);
            }
            
        }
    }

}
