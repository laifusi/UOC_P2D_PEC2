using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public static Action<int> OnFlagReached; // Action<int> for when the flag has been reached

    [SerializeField] HeightPoints[] heightPointsSectors; // HeightPoints array that defines the points the player gets in relation to the height the flag has been reached from

    [Serializable]
    private struct HeightPoints // struct that defines the height and points for each flag section
    {
        public int height;
        public int points;
    }

    /// <summary>
    /// OnCollisionEnter2D where we check what height the player hit the flag from and invoke the OnFlagReached Action with the amount of points that height gives us
    /// We also play the winning sound and set the flag reached animation trigger
    /// </summary>
    /// <param name="collision">Collision2D of the GameObject that hit us</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if(player != null)
        {
            GetComponent<Collider2D>().enabled = false;
            float contactPointY = collision.contacts[0].point.y;
            for(int i = 0; i < heightPointsSectors.Length; i++)
            {
                if(contactPointY < heightPointsSectors[i].height || i + 1 == heightPointsSectors.Length)
                {
                    OnFlagReached?.Invoke(heightPointsSectors[i].points);
                    GetComponent<AudioSource>().Play();
                    GetComponent<Animator>().SetTrigger("FlagReached");
                    return;
                }
            }
        }
    }
}
