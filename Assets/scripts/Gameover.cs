using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject gameOverCanvas;

    // Check for game over condition (e.g., player dies)
    public void GameOverScreen()
    {
        // Activate the game over canvas
        gameOverCanvas.SetActive(true);

        // Pause the game or freeze player input
        Time.timeScale = 0f;
    }

    // Restart the game
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the main menu
    public void LoadMainMenu()
    {
        // Load the main menu scene (you need to create this scene)
        SceneManager.LoadScene("Untitled");
    }
}

