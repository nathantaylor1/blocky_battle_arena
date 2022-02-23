using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int numEnemies = 100;
    
    private void Awake()
    {
        instance = this;
        MapGen.instance.seed = DataCarryOver.GameSeed;
        DataCarryOver.GameSeed = "Random"; // reset the seed after use

        if (DataCarryOver.StillAlive)
        {
            // Health:
            Player_Health.instance.health = DataCarryOver.PlayerHP;
            Player_Health.instance.UpdateHPText();
            // Score:
            PlayerScore.instance.score = DataCarryOver.PlayerScore;
            PlayerScore.instance.UpdateScoreText();
        }

        MapGen.instance.StartMapGen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            NextLevel();
    }

    private void NextLevel()
    {
        DataCarryOver.IncrementLevelCount();
        DataCarryOver.PlayerScore = PlayerScore.instance.score;
        DataCarryOver.StillAlive = true;
        DataCarryOver.PlayerHP = (int)Player_Health.instance.health;
        SceneManager.LoadScene("MainScene");
    }

    public void EnemyKilled()
    {
        numEnemies--;
        if (numEnemies <= 0)
        {
            NextLevel();
        }
    }
}
