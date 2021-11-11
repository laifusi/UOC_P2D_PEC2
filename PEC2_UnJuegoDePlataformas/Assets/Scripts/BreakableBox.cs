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
            Destroy(gameObject);
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
}
