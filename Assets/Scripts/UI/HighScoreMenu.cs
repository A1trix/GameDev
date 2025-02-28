using TMPro;
using UnityEngine;

public class HighscoreMenu : MonoBehaviour
{
    public TextMeshProUGUI highscoreText; // Assign in Inspector
    public string[] levelNames = new string[] { "Level 1", "Level 2"}; // Assign level scene names (e.g., "Level1", "Level2")

    private void Start()
    {
        // Automatically load highscores when the menu is opened
        LoadAndDisplayHighscores();
    }

    // Call this method if you have a "Refresh" button
    public void LoadAndDisplayHighscores()
    {
        var highscores = SaveSystem.LoadHighscores();
        highscoreText.text = "";

        Debug.Log("Loaded highscores:");

        foreach (string level in levelNames)
        {
            if (highscores.ContainsKey(level))
            {
                highscoreText.text += $"{level}: {FormatTime(highscores[level])}\n";
            }
            else
            {
                highscoreText.text += $"{level}: N/A\n";
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
    }
}