using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    private RaycastHit2D sensorHit;
    private Transform currentSensor;

    [SerializeField] int velocity = 1;
    [Range(-1, 1)][SerializeField] int xDirection = -1;
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] float sensorDistanceCheck = 0.1f;
    [SerializeField] LayerMask wallLayerMask;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();

        spriteRenderer.flipX = xDirection > 0;
        currentSensor = xDirection > 0 ? rightSensor : leftSensor;
        if(xDirection > 0)
            collider2d.offset *= new Vector2(-1, 1);
    }

    private void Update()
    {
        rigidbody2d.velocity = new Vector2(xDirection * velocity, rigidbody2d.velocity.y);

        sensorHit = Physics2D.Raycast(currentSensor.position, new Vector2(xDirection, 0), sensorDistanceCheck, wallLayerMask);
        if(sensorHit)
        {
            ChangeDirection();
        }
        
        /*sensorHit = Physics2D.Raycast(currentSensor.position, Vector2.down, sensorDistanceCheck);
        if (!sensorHit)
        {
            ChangeDirection();
        }*/
    }

    private void ChangeDirection()
    {
        xDirection *= -1;
        spriteRenderer.flipX = xDirection > 0;
        currentSensor = xDirection > 0 ? rightSensor : leftSensor;
        collider2d.offset *= new Vector2(-1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerMovement>();
        if(player != null)
        {
            if(collision.contacts[0].normal.y <= -0.5)
            {
                Die();
            }
            else
            {
                player.TakeDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        StartCoroutine(Fade());
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        enabled = false;
    }

    private IEnumerator Fade()
    {
        float alpha = 1;

        while(alpha > 0)
        {
            yield return new WaitForSeconds(0f);
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }
}
