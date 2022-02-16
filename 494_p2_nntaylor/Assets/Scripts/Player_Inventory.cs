using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player_Inventory : MonoBehaviour
{
    public static Player_Inventory instance;
    private bool _hasPickaxe = false, _hasSword = false;

    public void UpdateHasPickaxe() { _hasPickaxe = true; }
    public bool HasSword() { return _hasSword; }

    public int score = 0;
    public Text scoreText;
    public Text scoreText2;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PowerUp"))
        {
            Item pup = col.gameObject.GetComponent<Item>();

            if (pup.itemType == Items.PickaxePowerUp)
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
            else if (pup.itemType == Items.SwordPowerUp)
            {
                _hasSword = true;
                col.gameObject.SetActive(false);
            }
        }
        else if (col.gameObject.CompareTag("Coin"))
        {
            Item coin = col.gameObject.GetComponent<Item>();
            score += coin.worth;
            UpdateScoreText();
            col.gameObject.SetActive(false);
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
    }

}
