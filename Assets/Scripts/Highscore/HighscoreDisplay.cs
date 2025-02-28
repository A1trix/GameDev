// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class HighscoreDisplay : MonoBehaviour
// {
//     public TextMeshProUGUI highscoreText;

//     private void Start()
//     {
//         // Load the highscore for the current level
//         string currentLevel = SceneManager.GetActiveScene().name;
//         var highscores = SaveSystem.LoadHighscores();

//         if (highscores.ContainsKey(currentLevel))
//         {
//             highscoreText.text = $"Best Time: {highscores[currentLevel]:F2} seconds";
//         }
//         else
//         {
//             highscoreText.text = "Best Time: N/A";
//         }
//     }
// }