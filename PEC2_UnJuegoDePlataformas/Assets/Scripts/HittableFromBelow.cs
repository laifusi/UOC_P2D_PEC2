using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    protected bool hittable = true; // bool that indicates if the box is hittable

    [SerializeField] protected GameObject[] possiblePrefabs; // GameObject array of prefabs that can come out of the box

    /// <summary>
    /// OnCollisionEnter2D method where we check if the box is hittable and the player hit it
    /// If it is, we check if it was hit from below and we call the GotHitFromBelow method
    /// </summary>
    /// <param name="collision">Collision2D that hit the box</param>
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

    public abstract void GotHitFromBelow(bool playerIsSuper); // abstract method to define what each different HittableFromBelow object should do when hit

    /// <summary>
    /// Method to choose a random prefab and make it come out of the box that has been hit
    /// </summary>
    protected void PrefabOutOfBox()
    {
        int randomInt = Random.Range(0, possiblePrefabs.Length);
        var gameObject = Instantiate(possiblePrefabs[randomInt], transform.position, Quaternion.identity, transform);
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("OutOfBox");
    }
}
