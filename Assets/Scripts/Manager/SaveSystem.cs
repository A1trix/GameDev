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

// using UnityEngine;
// using System.IO;
// using System.Runtime.Serialization.Formatters.Binary;

// public static class SaveSystem{

//   public static void SavePlayer(PlayerBase player)
//   {
//     BinaryFormatter formatter = new BinaryFormatter();
//     string path = Application.persistentDataPath + "/PlayerData.dat";
//     FileStream stream = File.Open(path, FileMode.Create);

//     PlayerData data = new PlayerData(player);

//     formatter.Serialize(stream, data);
//     stream.Close();
//   }

//   public static PlayerData LoadPlayer()
//   {
//     string path = Application.persistentDataPath + "/PlayerData.dat";
//     if (File.Exists(path))
//     {
//       BinaryFormatter formatter = new BinaryFormatter();
//       FileStream stream = File.Open(path, FileMode.Open);

//       PlayerData data = formatter.Deserialize(stream) as PlayerData;
//       stream.Close();

//       return data;
//     }
//     else
//     {
//       Debug.Log("Save file not found in " + path);
//       return null;
//     }
//   }

//   // Loading
//   public static bool SaveFileExists()
//   {
//     string path = Application.persistentDataPath + "/PlayerData.dat";
//     return File.Exists(path);
//   }

//   // !!! DEBUG Not for use in production !!!
//   private static string savePath = Path.Combine(Application.persistentDataPath, "PlayerData.dat");

//   public static void DeleteSaveData()
//   {
//     if (File.Exists(savePath))
//     {
//       File.Delete(savePath);
//       Debug.Log("Save file deleted.");
//     }
//     else
//     {
//       Debug.Log("Save file not found.");
//     }
//   }

// }