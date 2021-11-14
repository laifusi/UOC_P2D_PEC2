using System;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    private int coinsCollected; // amount of coins collected

    public static Action<int> OnCoinCollected; // Action<int> for when a new coin has been collected

    public int TotalCoinsCollected => coinsCollected; // amount of coins collected accessible from other classes

    /// <summary>
    /// Start method where we reset the coin count and subscribe to the Coin's OnCoinPickedUp Action
    /// </summary>
    private void Start()
    {
        ResetCoins();
        Coin.OnCoinPickedUp += CollectCoin;
    }

    /// <summary>
    /// Method called when a coin is collected
    /// We increase the coinsCollected count and invoke the OnCoinCollected Action<int>
    /// </summary>
    private void CollectCoin()
    {
        coinsCollected++;
        OnCoinCollected?.Invoke(coinsCollected);
    }

    /// <summary>
    /// Method to reset the coins count
    /// Public in case we need to call it from another class
    /// </summary>
    public void ResetCoins()
    {
        coinsCollected = 0;
    }

    /// <summary>
    /// OnDisable method to unsubscribe from the Coin's OnCoinPickedUp Action
    /// </summary>
    private void OnDisable()
    {
        Coin.OnCoinPickedUp -= CollectCoin;
    }
}
