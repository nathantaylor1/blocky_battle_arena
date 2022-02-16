using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Player_Health.instance.health = (float) DataCarryOver.PlayerHP;
            Player_Health.instance.UpdateHPText();
            
            // Pickaxe:
            Player_Inventory.instance.UpdateHasPickaxe();
            Player_BreakBlock.instance.SetNumBlocksCanBreak((uint)DataCarryOver.NumberOfBreakableBlocks);
            Player_BreakBlock.instance.enabled = true;
            Player_BreakBlock.instance.UpdatePBBText();
            
            // Score:
            Player_Inventory.instance.score = DataCarryOver.PlayerScore;
            Player_Inventory.instance.UpdateScoreText();
        }

        MapGen.instance.StartMapGen();
    }

    private void NextLevel()
    {
        DataCarryOver.StillAlive = true;
        DataCarryOver.PlayerHP = (int)Player_Health.instance.health;
        DataCarryOver.NumberOfBreakableBlocks = Player_BreakBlock.instance.GetNumBlocksCanBreak();
        DataCarryOver.PlayerScore = Player_Inventory.instance.score;
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
