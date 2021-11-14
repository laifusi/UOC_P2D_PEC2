using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidbody2d; // Rigidbody2D component
    private SpriteRenderer spriteRenderer; // SpriteRenderer component
    private Collider2D collider2d; // Collider2D component
    private RaycastHit2D sensorHit; // RaycastHit2D for the enemy's sensor's hit
    private Transform currentSensor; // Transform of the active sensor
    private AudioSource audioSource; // AudioSource component

    [Header("Movement")]
    [SerializeField] int velocity = 1; // movement velocity
    [Range(-1, 1)][SerializeField] int xDirection = -1; // direction in the x axis
    [Header("Side sensors")]
    [SerializeField] Transform leftSensor; // left sensor's Transform
    [SerializeField] Transform rightSensor; // right sensor's Transform
    [SerializeField] float sensorDistanceCheck = 0.1f; // checking distance for the sensors
    [SerializeField] LayerMask wallLayerMask; // LayerMask for the walls the enemy has to turn around from

    public static Action OnEnemyKilled; // Action for when the enemy is killed

    /// <summary>
    /// Start method where we get the needed components
    /// We set the sprite renderer, the current sensor and the collider's offset in relation to the xDirection
    /// </summary>
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer.flipX = xDirection > 0;
        currentSensor = xDirection > 0 ? rightSensor : leftSensor;
        if(xDirection > 0)
            collider2d.offset *= new Vector2(-1, 1);
    }

    /// <summary>
    /// Update method to move the enemy and check the side sensor
    /// We change the direction if there's a wall in the direction of the enemy
    /// </summary>
    private void Update()
    {
        rigidbody2d.velocity = new Vector2(xDirection * velocity, rigidbody2d.velocity.y);

        sensorHit = Physics2D.Raycast(currentSensor.position, new Vector2(xDirection, 0), sensorDistanceCheck, wallLayerMask);
        if(sensorHit)
        {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Method to change the enemy's direction, the sensor, the collider's offset and the sprite renderer's direction
    /// </summary>
    private void ChangeDirection()
    {
        xDirection *= -1;
        spriteRenderer.flipX = xDirection > 0;
        currentSensor = xDirection > 0 ? rightSensor : leftSensor;
        collider2d.offset *= new Vector2(-1, 1);
    }

    /// <summary>
    /// OnCollisionEnter2D method to check if we collided with a player
    /// If we did, we check if the contact was made from the top
    /// If it was, the enemy dies
    /// If we didn't, the player takes damage
    /// </summary>
    /// <param name="collision">Collision2D of the GameObject we collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
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

    /// <summary>
    /// OnTriggerEnter2D to destroy the GameObject if the enemy falls off to the void
    /// </summary>
    /// <param name="collision">Collider2D of the trigger we hit</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method to play the enemy hit sound, invoke the enemy killed Action, start the fading coroutine,
    /// disabling the collider, setting the death animation trigger, changing the rigidbody to static and disabling the script
    /// </summary>
    private void Die()
    {
        audioSource.Play();
        OnEnemyKilled?.Invoke();
        StartCoroutine(Fade());
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        enabled = false;
    }

    /// <summary>
    /// Coroutine to fade out the enemy
    /// </summary>
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
