using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : HittableFromBelow
{
    public static Action OnBoxBroken;

    private Animator animator;
    private int amountOfCoins;
    private int coinsTaken;

    [SerializeField] int maxPossibleAmountOfCoins = 5;
    [SerializeField] GameObject particles;

    private void Start()
    {
        animator = GetComponent<Animator>();
        amountOfCoins = UnityEngine.Random.Range(0, maxPossibleAmountOfCoins);
    }

    public override void GotHitFromBelow(bool playerIsSuper)
    {
        if(playerIsSuper)
        {
            OnBoxBroken?.Invoke();
            StartCoroutine(DestroyBox());
        }
        else
        {
            animator.SetTrigger("HitBox");
            if(coinsTaken < amountOfCoins)
            {
                PrefabOutOfBox();
                coinsTaken++;
            }
        }
    }

    private IEnumerator DestroyBox()
    {
        particles.SetActive(true);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return null;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
