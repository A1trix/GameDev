using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Makes the SceneController a singleton -> only one instance of the class can exist
    public static SceneController Instance { get; private set; }
    private bool isRestarting;

    // If there is no existing instance, this object becomes the singleton instance and is not destroyed when loading a new scene (DontDestroyOnLoad).
    // It registers the OnSceneLoaded method to the sceneLoaded event.
    // If an instance already exists, the new one is destroyed to maintain a single instance.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("Destroyed duplicate SceneController instance");
        }
    }

    // When this object is destroyed, it removes itself from the sceneLoaded event to avoid errors.
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Sets isRestarting = true so it knows the level is being restarted.
    // Calls ReloadCurrentScene() to restart the level.
    public void HandlePlayerDeath()
    {
        isRestarting = true;
        ReloadCurrentScene();
    }

    // If the level was restarted (isRestarting == true), it resets 
    // the player by calling ResetToDefaultValues() on the PlayerBase object.
    // Resets the PauseMenu from the previous Scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        if (isRestarting)
        {
            PlayerBase player = FindObjectOfType<PlayerBase>();
            if (player != null) player.ResetToDefaultValues();
            isRestarting = false;
        }
    }

    // Checks if the scene exists and loads it asynchronously.
    // If it doesn’t exist, logs an error and loads Level 1.
    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // Get the currently active scene before loading the new one
            Scene currentScene = SceneManager.GetActiveScene();

            // Load the new scene asynchronously
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            // Ensure the old scene (e.g., Main Menu) is unloaded
            if (currentScene.name == "Main Menu")
            {
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }
        else
        {
            Debug.LogError($"Scene {sceneName} not found in build settings!");
            LoadDefaultScene();
        }
    }


    // Gets the curret scene index and loads it asynchronously.
    public void ReloadCurrentScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // Loads "Level 1" as a fallback in case of errors.
    private void LoadDefaultScene()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    // Gets the next level's index by adding 1 to the current scene’s build index.
    // Loads it if it's within the total number of scenes in the build settings.
    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextIndex);
        }
        else 
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    // Checks if a saved game exists (SaveSystem.SaveFileExists()).
    // If a save exists, it loads the saved level (HandleLevelTransition(data.level)).
    // If there's no save, it starts a new game at "Level 1".
    public void OnPlayButtonClicked()
    {
        Time.timeScale = 1f;
        
        if (SaveSystem.SaveFileExists())
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data != null)
            {
                LoadScene(data.sceneName);
            }
            else
            {
                LoadDefaultScene();
            }
        }
        else
        {
            LoadDefaultScene();
        }
    }
}