using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour {
    
    // Load the next scene of the game
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Update the responsinevess of the Helicopter based on the selected difficulty and then run the game
    public void EasyDifficulty() {
        DifficultyManager.instance.responsinevess = 25000;
        StartGame();
    }

    public void AverageDifficulty() {
        DifficultyManager.instance.responsinevess = 250000;
        StartGame();
    }

    public void HardDifficulty() {
        DifficultyManager.instance.responsinevess = 2500000;
        StartGame();
    }
}
