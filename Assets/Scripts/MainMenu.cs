using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    // Load the next scene of the game
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Load the 1st scene of the game
    public void RestartGame() {
        SceneManager.LoadScene(0);
    }


    // Quit the game
    public void QuitGame() {
        Application.Quit();
    }
}
