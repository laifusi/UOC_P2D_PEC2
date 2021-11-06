using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : HittableFromBelow
{
    public override void GotHitFromBelow(bool playerIsSuper)
    {
        if(playerIsSuper)
        {
            Debug.Log("Break block");
            Destroy(gameObject);
        }
    }
}
