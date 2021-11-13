using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    protected bool hittable = true;

    [SerializeField] protected GameObject[] possiblePrefabs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null || !hittable)
            return;

        if(collision.contacts[0].normal.y >= 0.5f)
        {
            GotHitFromBelow(player.IsSuper);
        }
    }

    public abstract void GotHitFromBelow(bool playerIsSuper);

    protected void PrefabOutOfBox()
    {
        int randomInt = Random.Range(0, possiblePrefabs.Length);
        var gameObject = Instantiate(possiblePrefabs[randomInt], transform.position, Quaternion.identity, transform);
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("OutOfBox");
    }
}
