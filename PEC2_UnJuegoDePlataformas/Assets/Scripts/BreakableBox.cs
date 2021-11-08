using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : HittableFromBelow
{
    public static Action OnBoxBroken;

    public override void GotHitFromBelow(bool playerIsSuper)
    {
        if(playerIsSuper)
        {
            OnBoxBroken?.Invoke();
            Destroy(gameObject);
        }
    }
}
