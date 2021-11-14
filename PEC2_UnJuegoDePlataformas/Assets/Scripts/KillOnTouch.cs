using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter2D to kill the player if it hits a KillOnTouch trigger
    /// </summary>
    /// <param name="collision">Collider2D that triggered the method</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if(player != null)
        {
            player.Die();
        }
    }

    /// <summary>
    /// OnCollisionEnter2D to killed the player if it hits a KillOnTouch collider
    /// </summary>
    /// <param name="collision">Collision2D that hit the collider</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            player.Die();
        }
    }
}
