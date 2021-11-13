using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] int pointsPerCoin = 200;
    [SerializeField] int pointsPerPowerUp = 1000;
    [SerializeField] int pointsPerEnemyKilled = 100;
    [SerializeField] int pointsPerBrokenBox = 50;

    private int totalPoints;

    public static Action<int> OnPointsChanged;
    public static Action<int> OnPointsAdded;
    public static Action OnGameWon;

    public int TotalPoints => totalPoints;

    private void Start()
    {
        ResetPoints();

        Coin.OnCoinPickedUp += CoinPickedUp;
        Player.OnPowerUpPickedUp += PowerUpPickedUp;
        Enemy.OnEnemyKilled += EnemyKilled;
        BreakableBox.OnBoxBroken += BoxBroken;
        Flag.OnFlagReached += FlagReached;
    }

    public void ResetPoints()
    {
        totalPoints = 0;
    }

    private void CoinPickedUp()
    {
        totalPoints += pointsPerCoin;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerCoin);
    }

    private void PowerUpPickedUp()
    {
        totalPoints += pointsPerPowerUp;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerPowerUp);
    }

    private void EnemyKilled()
    {
        totalPoints += pointsPerEnemyKilled;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerEnemyKilled);
    }

    private void BoxBroken()
    {
        totalPoints += pointsPerBrokenBox;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(pointsPerBrokenBox);
    }

    private void FlagReached(int flagPoints)
    {
        totalPoints += flagPoints;
        OnPointsChanged?.Invoke(totalPoints);
        OnPointsAdded?.Invoke(flagPoints);
        OnGameWon?.Invoke();
    }

    private void OnDisable()
    {
        Coin.OnCoinPickedUp -= CoinPickedUp;
        Player.OnPowerUpPickedUp -= PowerUpPickedUp;
        Enemy.OnEnemyKilled -= EnemyKilled;
        BreakableBox.OnBoxBroken -= BoxBroken;
        Flag.OnFlagReached -= FlagReached;
    }
}
