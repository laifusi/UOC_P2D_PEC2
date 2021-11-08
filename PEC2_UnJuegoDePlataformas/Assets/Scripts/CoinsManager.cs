using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    private int coinsCollected;

    public static Action<int> OnCoinCollected;

    private void Start()
    {
        ResetCoins();
        Coin.OnCoinPickedUp += CollectCoin;
    }

    private void CollectCoin()
    {
        coinsCollected++;
        OnCoinCollected?.Invoke(coinsCollected);
    }

    public void ResetCoins()
    {
        coinsCollected = 0;
    }

    private void OnDisable()
    {
        Coin.OnCoinPickedUp -= CollectCoin;
    }
}
