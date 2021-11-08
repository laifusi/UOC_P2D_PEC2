using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action OnCoinPickedUp;

    public void PickUpCoin()
    {
        OnCoinPickedUp?.Invoke();
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if(player != null)
        {
            PickUpCoin();
            DestroyCoin();
        }
    }
}
