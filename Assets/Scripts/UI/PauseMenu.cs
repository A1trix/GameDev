using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public static bool GameIsPaused = false;
  public GameObject PauseMenuUI;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPaused)
      {
        Resume();
      }
      else 
      {
        Pause();
      }
    }
  }

  private void OnDestroy()
  {
      GameIsPaused = false;
      Time.timeScale = 1f;
  }

    public void Resume()
  {
    PauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
    Debug.Log("Resume");
  }

  public void Pause()
  {
    PauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
    Debug.Log("Pause");
  }

  public void Home()
  {
    // Reset the time scale before loading the new scene
    Time.timeScale = 1f;
    GameIsPaused = false;
    Debug.Log("Home");

    // Deactivate the pause menu
    PauseMenuUI.SetActive(false);

    // Load the main menu scene
    SceneManager.LoadScene("Main Menu");
  }
}
