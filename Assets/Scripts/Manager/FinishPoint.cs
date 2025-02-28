using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBase player = collision.GetComponent<PlayerBase>();
            if (player != null)
            {
                float currentTime = player.GetElapsedTime();
                SaveHighscore(currentTime);
                player.SavePlayer();
                SceneController.Instance.LoadNextLevel();
            }
            else
            {
                SceneController.Instance.LoadScene("Main Menu");
            }
        }
    }

    private void SaveHighscore(float currentTime)
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        var highscores = SaveSystem.LoadHighscores();

        if (!highscores.ContainsKey(currentLevel) || currentTime < highscores[currentLevel])
        {
            highscores[currentLevel] = currentTime;
            SaveSystem.SaveHighscores(highscores);
        }
    }
}