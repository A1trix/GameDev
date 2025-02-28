using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string playerSavePath = Application.persistentDataPath + "/player.save";
    private static string highscoreSavePath = Application.persistentDataPath + "/highscores.save";

    // Save player data
    public static void SavePlayer(PlayerBase player)
    {
        PlayerData data = new PlayerData(player);
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(playerSavePath, jsonData);
    }

    // Load player data
    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerSavePath))
        {
            string jsonData = File.ReadAllText(playerSavePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return null;
    }

    // Check if player save file exists
    public static bool SaveFileExists()
    {
        return File.Exists(playerSavePath);
    }

    // Delete player save data
    public static void DeleteSaveData()
    {
        if (File.Exists(playerSavePath))
        {
            File.Delete(playerSavePath);
            File.Delete(highscoreSavePath);
        }
    }

    // Save highscores
    public static void SaveHighscores(Dictionary<string, float> highscores)
    {
        List<HighscoreData> highscoreList = new List<HighscoreData>();
        foreach (var entry in highscores)
        {
            highscoreList.Add(new HighscoreData(entry.Key, entry.Value));
        }
        string jsonData = JsonUtility.ToJson(new HighscoreWrapper(highscoreList));
        File.WriteAllText(highscoreSavePath, jsonData);
    }

    // Load highscores
    public static Dictionary<string, float> LoadHighscores()
    {
        if (File.Exists(highscoreSavePath))
        {
            string jsonData = File.ReadAllText(highscoreSavePath);
            HighscoreWrapper wrapper = JsonUtility.FromJson<HighscoreWrapper>(jsonData);
            Dictionary<string, float> highscores = new Dictionary<string, float>();
            foreach (var data in wrapper.highscores)
            {
                highscores[data.levelName] = data.bestTime;
            }
            return highscores;
        }
        return new Dictionary<string, float>();
    }

    // Wrapper class for serialization
    [System.Serializable]
    private class HighscoreWrapper
    {
        public List<HighscoreData> highscores;

        public HighscoreWrapper(List<HighscoreData> highscores)
        {
            this.highscores = highscores;
        }
    }
}