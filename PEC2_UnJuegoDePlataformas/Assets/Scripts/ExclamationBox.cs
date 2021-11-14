using UnityEngine;

public class ExclamationBox : HittableFromBelow
{
    [SerializeField] Sprite usedSprite; // used exclamation box Sprite

    /// <summary>
    /// GotHitFromBelow method inherited from HittableFromBelow
    /// We make a prefab come out of the box and set the box to a used state
    /// </summary>
    /// <param name="playerIsSuper"></param>
    public override void GotHitFromBelow(bool playerIsSuper)
    {
        PrefabOutOfBox();
        UsedBlock();
    }

    /// <summary>
    /// Method to change the sprite to the usedSprite and set the hittable inherited variable to false
    /// </summary>
    private void UsedBlock()
    {
        GetComponent<SpriteRenderer>().sprite = usedSprite;
        hittable = false;
    }
}
