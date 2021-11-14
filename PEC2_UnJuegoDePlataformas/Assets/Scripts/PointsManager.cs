using System;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] int pointsPerCoin = 200; // amount of points per coin collected
    [SerializeField] int pointsPerPowerUp = 1000; // amount of points per power up collected
    [SerializeField] int pointsPerEnemyKilled = 100; // amount of points per enemy killed
    [SerializeField] int pointsPerBrokenBox = 50; // amount of points per box broken

    private int totalPoints; // total amount of points
    
    public static Action<int> OnPointsChanged; // Action<int> for when the amount of total points changes (we pass the total points)
    public static Action<int> OnPointsAdded; // Action<int> for when we add an amount of points (we pass the number of points added)
    public static Action OnGameWon; // Action for when the game is won

    public int TotalPoints => totalPoints; // public amount of total points

    /// <summary>
    /// Start method to reset the points and subscribe to the Actions that give points
    /// </summary>
    private void Start()
    {
        ResetPoints();

        Coin.OnCoinPickedUp += CoinPickedUp;
        Player.OnPowerUpPickedUp += PowerUpPickedUp;
        Enemy.OnEnemyKilled += EnemyKilled;
        BreakableBox.OnBoxBroken += BoxBroken;
        Flag.OnFlagReached += FlagReached;
    }

    /// <summary>
    /// Method to set the points to 0
    /// Public in case we need to call it from another class
    /// </summary>
    public void ResetPoints()
    {
        totalPoints = 0;
    }

    /// <summary>
    /// Method to add the points for a coin collected
    /// </summary>
    private void CoinPickedUp()
    {
        totalPoints += pointsPerCoin;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerCoin);
    }

    /// <summary>
    /// Method to add the points for a power up collected
    /// </summary>
    private void PowerUpPickedUp()
    {
        totalPoints += pointsPerPowerUp;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerPowerUp);
    }

    /// <summary>
    /// Method to add the points for an enemy killed
    /// </summary>
    private void EnemyKilled()
    {
        totalPoints += pointsPerEnemyKilled;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerEnemyKilled);
    }

    /// <summary>
    /// Method to add the points for a box broken
    /// </summary>
    private void BoxBroken()
    {
        totalPoints += pointsPerBrokenBox;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerBrokenBox);
    }

    /// <summary>
    /// Method to add the points for the flag reached and invoke the OnGameWon Action
    /// </summary>
    /// <param name="flagPoints">amount of points to be added</param>
    private void FlagReached(int flagPoints)
    {
        totalPoints += flagPoints;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(flagPoints);
        OnGameWon?.Invoke();
    }

    /// <summary>
    /// OnDisable method to unsubscribe to the Actions
    /// </summary>
    private void OnDisable()
    {
        Coin.OnCoinPickedUp -= CoinPickedUp;
        Player.OnPowerUpPickedUp -= PowerUpPickedUp;
        Enemy.OnEnemyKilled -= EnemyKilled;
        BreakableBox.OnBoxBroken -= BoxBroken;
        Flag.OnFlagReached -= FlagReached;
    }
}
