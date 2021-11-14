using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesManager : MonoBehaviour
{
    /// <summary>
    /// Method to load the Game scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Method to load the Menu scene
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Method to exit the game
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
