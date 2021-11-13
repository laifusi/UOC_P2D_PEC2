using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action OnCoinPickedUp;

    public bool pickedup;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickUpCoin()
    {
        OnCoinPickedUp?.Invoke();
        audioSource.Play();
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if(player != null && !pickedup)
        {
            pickedup = true;
            StartCoroutine(PickUpAndDestroy());
        }
    }

    IEnumerator PickUpAndDestroy()
    {
        PickUpCoin();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        DestroyCoin();
    }
}
