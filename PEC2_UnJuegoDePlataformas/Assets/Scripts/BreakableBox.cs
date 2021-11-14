using System;
using System.Collections;
using UnityEngine;

public class BreakableBox : HittableFromBelow
{
    public static Action OnBoxBroken; // Action for when the box is broken

    private Animator animator; // Animator component
    private AudioSource audioSource; // AudioSource component
    private int amountOfCoins; // number of coins the box has (random number between 0 and the maxPossibleAmountOfCoins)
    private int coinsTaken; // number of coins already picked up from the box

    [SerializeField] int maxPossibleAmountOfCoins = 5; // maximum possible number of coins in the box
    [SerializeField] GameObject particles; // GameObject containing the ParticleSystem of the box breaking

    /// <summary>
    /// Start method where we get the Animator and AudioSource components and randomize the number of coins in the box
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        amountOfCoins = UnityEngine.Random.Range(0, maxPossibleAmountOfCoins);
    }

    /// <summary>
    /// GotHitFromBelow method inherited from HittableFromBelow
    /// We check if the play is in super mode
    /// If they are, we break the box
    /// If they aren't, we check if there's coins to pick up and we do
    /// </summary>
    /// <param name="playerIsSuper">bool that indicates if the player is in super mode</param>
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

    /// <summary>
    /// Coroutine to play the particle system and the audio clip before destroying the box
    /// </summary>
    private IEnumerator DestroyBox()
    {
        particles.SetActive(true);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
