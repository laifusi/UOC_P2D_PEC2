using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action OnCoinPickedUp; // Action for when the coin has been picked up

    public bool pickedUp; // bool that indicates if the coin has been picked up
    
    private AudioSource audioSource; // AudioSource component

    /// <summary>
    /// Awake method where we get the AudioSource component
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Method to invoke the OnPickedUp Action and play the audio clip
    /// Public to be called from the "OutOfBox" animation
    /// </summary>
    public void PickUpCoin()
    {
        OnCoinPickedUp?.Invoke();
        audioSource.Play();
    }

    /// <summary>
    /// Method to destroy the GameObject
    /// Public to be called from the "OutOfBox" animation
    /// </summary>
    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// OnTriggerEnter2D to detect if the player has reached a coin
    /// If he has, we pick up the coin and destroy it
    /// </summary>
    /// <param name="collision">Collider2D that entered the trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if(player != null && !pickedUp)
        {
            pickedUp = true;
            StartCoroutine(PickUpAndDestroy());
        }
    }

    /// <summary>
    /// Coroutine where we pick up the coin, wait 0.5 seconds and destroy the coin
    /// </summary>
    IEnumerator PickUpAndDestroy()
    {
        PickUpCoin();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        DestroyCoin();
    }
}
