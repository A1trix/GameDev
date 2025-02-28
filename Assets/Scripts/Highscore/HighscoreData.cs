using System;
using UnityEngine;

[System.Serializable]
public class HighscoreData
{
    public string levelName; // Name of the level (scene)
    public float bestTime;   // Best time for the level

    public HighscoreData(string levelName, float bestTime)
    {
        this.levelName = levelName;
        this.bestTime = bestTime;
    }
}