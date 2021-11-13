using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] CoinsManager coinsManager;
    [SerializeField] PointsManager pointsManager;
    [SerializeField] TimeManager timeManager;

    private void Start()
    {
        Time.timeScale = 1;
        Player.OnDied += GameLost;
        PointsManager.OnGameWon += GameWon;
    }

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

        if(timePassed < timeHighscore)
        {
            PlayerPrefs.SetInt("Time", timePassed);
        }

        if(totalPoints > pointsHighscore)
        {
            PlayerPrefs.SetInt("Points", totalPoints);
        }

        Time.timeScale = 0;
    }

    private void GameLost()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Player.OnDied -= GameLost;
        PointsManager.OnGameWon -= GameWon;
    }
}
