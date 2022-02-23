using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarryOver : MonoBehaviour
{
    public static string GameSeed = "random";
    
    public static bool StillAlive = false;
    public static int PlayerHP = 10;
    public static int PlayerScore = 0;
    
    private static int levelCount = 0;
    
    public static void IncrementLevelCount()
    {
        ++levelCount;
        if (levelCount == 4)
            STATIC_AudioManager.instance.PlayLateGameAudio();
    }

    public static int GetLevelCount()
    {
        return levelCount;
    }

    public static void ResetData()
    {
        print("resetting");
        STATIC_AudioManager.instance.PlayGameAudio();
        StillAlive = false;
        PlayerScore = 0;
        PlayerHP = 10;
        levelCount = 0;
    }
}
