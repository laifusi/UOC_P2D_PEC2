using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationBox : HittableFromBelow
{
    [SerializeField] GameObject[] possiblePrefabs;
    [SerializeField] Sprite usedSprite;

    public override void GotHitFromBelow(bool playerIsSuper)
    {
        int randomInt = Random.Range(0, possiblePrefabs.Length);
        var gameObject = Instantiate(possiblePrefabs[randomInt], transform.position, Quaternion.identity, transform);
        Animator animator = gameObject.GetComponent<Animator>();
        if(animator != null)
            animator.SetTrigger("OutOfBox");
        UsedBlock();
    }

    private void UsedBlock()
    {
        GetComponent<SpriteRenderer>().sprite = usedSprite;
        hittable = false;
    }
}
