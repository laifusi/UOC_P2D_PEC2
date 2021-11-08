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

    private void Start()
    {
        ResetPoints();

        Coin.OnCoinPickedUp += CoinPickedUp;
        PlayerMovement.OnPowerUpPickedUp += PowerUpPickedUp;
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
    }

    private void PowerUpPickedUp()
    {
        totalPoints += pointsPerPowerUp;
        OnPointsChanged?.Invoke(totalPoints);
    }

    private void EnemyKilled()
    {
        totalPoints += pointsPerEnemyKilled;
        OnPointsChanged?.Invoke(totalPoints);
    }

    private void BoxBroken()
    {
        totalPoints += pointsPerBrokenBox;
        OnPointsChanged?.Invoke(totalPoints);
    }

    private void FlagReached(int flagPoints)
    {
        totalPoints += flagPoints;
        OnPointsChanged?.Invoke(totalPoints);
    }

    private void OnDisable()
    {
        Coin.OnCoinPickedUp -= CoinPickedUp;
        PlayerMovement.OnPowerUpPickedUp -= PowerUpPickedUp;
        Enemy.OnEnemyKilled -= EnemyKilled;
        BreakableBox.OnBoxBroken -= BoxBroken;
        Flag.OnFlagReached -= FlagReached;
    }
}
