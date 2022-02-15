using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public static Player_Inventory instance;
    private bool _hasPickaxe = false, _hasSword = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("yo");
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
                    col.gameObject.SetActive(false);
                }
                else
                {
                    pbb.AddNumBlocksCanBreak(25);
                    col.gameObject.SetActive(false);
                }
            }
            else if (pup.thisPowerUp == Items.SwordPowerUp)
            {
                // TODO: Enable Sword
            }
            
        }
    }

}
