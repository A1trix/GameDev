using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private PlayerBase player;

    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    private void Update()
    {
        if (player != null)
        {
            float elapsedTime = player.GetElapsedTime();
            timerText.text = FormatTime(elapsedTime);
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