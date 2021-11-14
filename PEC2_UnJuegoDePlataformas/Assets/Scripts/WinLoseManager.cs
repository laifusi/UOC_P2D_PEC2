using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    [SerializeField] GameObject losePanel; // lost game panel
    [SerializeField] GameObject winPanel; // won game panel
    [SerializeField] CoinsManager coinsManager; // CoinsManager
    [SerializeField] PointsManager pointsManager; // PointsManager
    [SerializeField] TimeManager timeManager; // TimeManager

    /// <summary>
    /// Start method where we set the timeScale to 1 and we subscribe to the Player's OnDied Action and the PointsManager's OnGameWon Action
    /// </summary>
    private void Start()
    {
        Time.timeScale = 1;
        Player.OnDied += GameLost;
        PointsManager.OnGameWon += GameWon;
    }

    /// <summary>
    /// Method to act when the game has been won
    /// We activate the winPanel and check if there's a new highscore
    /// We change the timeScale to 0, so that the game stops
    /// </summary>
    private void GameWon()
    {
        winPanel.SetActive(true);
        int coinsHighscore = PlayerPrefs.GetInt("Coins");
        int timeHighscore = PlayerPrefs.GetInt("Time");
        int pointsHighscore = PlayerPrefs.GetInt("Points");
        int coinsCollected = coinsManager.TotalCoinsCollected;
        int timePassed = (int)timeManager.TotalTimePassed;
        int totalPoints = pointsManager.TotalPoints;
        if (coinsCollected > coinsHighscore)
        {
            PlayerPrefs.SetInt("Coins", coinsCollected);
        }

        if(timePassed < timeHighscore || timeHighscore == 0)
        {
            PlayerPrefs.SetInt("Time", timePassed);
        }

        if(totalPoints > pointsHighscore)
        {
            PlayerPrefs.SetInt("Points", totalPoints);
        }

        Time.timeScale = 0;
    }

    /// <summary>
    /// Method to act when the game has been lost
    /// We activate the losePanel and set the timeScale to 0, to stop the game
    /// </summary>
    private void GameLost()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// OnDisable method to unsubscribe from the Actions
    /// </summary>
    private void OnDisable()
    {
        Player.OnDied -= GameLost;
        PointsManager.OnGameWon -= GameWon;
    }
}
