using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player; // Transform from the player

    private float offset; // the constant offset from the x position from the camera to the player

    /// <summary>
    /// Start method where we get the offset
    /// </summary>
    private void Start()
    {
        offset = transform.position.x - player.transform.position.x;
    }

    /// <summary>
    /// Update method to move the camera with the player
    /// </summary>
    private void Update()
    {
        float x = transform.position.x - player.transform.position.x;
        transform.Translate(offset - x, 0, 0);
    }
}
