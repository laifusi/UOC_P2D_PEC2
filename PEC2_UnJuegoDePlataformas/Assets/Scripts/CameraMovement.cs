using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    private float offset;

    private void Start()
    {
        offset = transform.position.x - player.transform.position.x;

    }

    private void Update()
    {
        float x = transform.position.x - player.transform.position.x;
        transform.Translate(offset - x, 0, 0);
    }
}
