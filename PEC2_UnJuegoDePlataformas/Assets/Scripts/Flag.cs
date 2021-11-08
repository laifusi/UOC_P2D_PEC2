using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public static Action<int> OnFlagReached;

    [SerializeField] HeightPoints[] heightPointsSectors;

    [Serializable]
    private struct HeightPoints
    {
        public int height;
        public int points;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerMovement>();
        if(player != null)
        {
            GetComponent<Collider2D>().enabled = false;
            float contactPointY = collision.contacts[0].point.y;
            for(int i = 0; i < heightPointsSectors.Length; i++)
            {
                if(contactPointY < heightPointsSectors[i].height || i + 1 == heightPointsSectors.Length)
                {
                    OnFlagReached?.Invoke(heightPointsSectors[i].points);
                    GetComponent<Animator>().SetTrigger("FlagReached");
                    return;
                }
            }
        }
    }
}
