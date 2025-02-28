using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
  public void OnPlayButtonClicked()
  {
      // Delegate to SceneController
      SceneController.Instance.OnPlayButtonClicked();
  }

  public void QuitGame()
  {
      Application.Quit();
  }
}