using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationBox : HittableFromBelow
{
    [SerializeField] Sprite usedSprite;

    public override void GotHitFromBelow(bool playerIsSuper)
    {
        PrefabOutOfBox();
        UsedBlock();
    }

    private void UsedBlock()
    {
        GetComponent<SpriteRenderer>().sprite = usedSprite;
        hittable = false;
    }
}
