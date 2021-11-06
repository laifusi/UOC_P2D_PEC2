using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    protected bool hittable = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerMovement>();
        if (player == null || !hittable)
            return;

        if(collision.contacts[0].normal.y >= 0.5f)
        {
            GotHitFromBelow(player.IsSuper);
        }
    }

    public abstract void GotHitFromBelow(bool playerIsSuper);
}
