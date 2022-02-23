using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;
    public int score = 0;
    public Text scoreText, gameOverScoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString("000");
        gameOverScoreText.text = score.ToString("000");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            score += col.GetComponent<Item>().worth;
            UpdateScoreText();
            Destroy(col.gameObject);
        }
    }
}
