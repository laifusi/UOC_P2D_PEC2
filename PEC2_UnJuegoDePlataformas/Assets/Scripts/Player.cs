using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2d; // Rigidbody2D component
    private Animator animator; // Animator component
    private SpriteRenderer spriteRenderer; // SpriteRenderer component
    private float horizontal; // horizontal direction
    private bool super; // bool that indicates if the player is in super mode
    private bool invulnerable; // bool that indicates if the player is invulnerable
    private float timePassed; // timePassed to control longer jumps
    private bool onLeftInvisibleWall; // bool that indicates if we are touching an invisible wall on the left
    private AudioSource audioSource; // AudioSource component

    [Header("Movement")]
    [SerializeField] float velocity = 5; // movement velocity
    [Header("Jumping")]
    [SerializeField] float minJumpForce = 2; // minimum jump force
    [SerializeField] float jumpForceMultiplier = 2; // jump force multiplier
    [SerializeField] float maxJumpForce = 7; // maximum jump force
    [SerializeField] float maxJumpTime = 1; // maximum jumping time
    [Header("Feet sensor")]
    [SerializeField] Transform feet; // Transform for the feet
    [SerializeField] float groundedCheckRadius = 0.1f; // radius for the grounded check
    [SerializeField] LayerMask floorMask; // LayerMask for the floor
    [Header("Audio Clips")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip powerUpSound;

    public bool IsSuper => super; // public bool of the super mode

    public static Action OnPowerUpPickedUp; // Action for when a power up is picked up
    public static Action OnDied; // Action for when the player dies

    /// <summary>
    /// Start method where we get the needed components and set the super mode and the invulnerability to false
    /// </summary>
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        super = false;
        invulnerable = false;
    }

    /// <summary>
    /// Update method to move the player
    /// </summary>
    private void Update()
    {
        MovementControl();
        JumpControl();
    }

    /// <summary>
    /// Method to control the movement of the character
    /// We get the input, and move according to it
    /// We set the walking animation and flip the sprite when necessary
    /// </summary>
    private void MovementControl()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0 && onLeftInvisibleWall)
        {
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(horizontal * velocity, rigidbody2d.velocity.y);
        }

        animator.SetBool("walking", horizontal != 0);

        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    /// <summary>
    /// Method to control the jumping
    /// We check the input and jump if the character is grounded
    /// We set the animation to jumping if the player isn't grounded
    /// Then we check if the key is still being pressed,
    /// if the time passed from the current jump is less than the max jump time
    /// and if the jump force will still be less than the max jump force
    /// If all of that is true, we jump longer
    /// </summary>
    private void JumpControl()
    {
        timePassed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
            timePassed = 0;
        }
        else
        {
            animator.SetBool("jumping", !IsGrounded());
        }

        if (Input.GetKey(KeyCode.Space) && timePassed <= maxJumpTime && rigidbody2d.velocity.y + jumpForceMultiplier <= maxJumpForce)
        {
            JumpLonger();
        }
    }

    /// <summary>
    /// Method to jump the minimum amount and play the animation and the sound
    /// </summary>
    private void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, minJumpForce);
        animator.SetBool("jumping", true);
        audioSource.PlayOneShot(jumpSound);
    }

    /// <summary>
    /// Method to jump longer, we add the jumpForceMultiplier
    /// </summary>
    private void JumpLonger()
    {
        rigidbody2d.velocity += new Vector2(0, jumpForceMultiplier);
    }

    /// <summary>
    /// Method to check if the player is grounded
    /// </summary>
    /// <returns>true if on the ground, false if off the ground</returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, groundedCheckRadius, floorMask);
    }

    /// <summary>
    /// Method to take damage
    /// If the character isn't invulerable, we check the super mode
    /// If the character is in super mode, it gets hit, the super disappears and we start the invulnerability coroutine
    /// If the character isn't in super mode, they die
    /// </summary>
    public void TakeDamage()
    {
        if (invulnerable)
            return;

        if (super)
        {
            audioSource.PlayOneShot(hitSound);
            super = false;
            transform.localScale = new Vector3(1, 1, 1);
            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    /// <summary>
    /// Method to kill the character, we play the hit sound, set the super to false and invoke the OnDied Action
    /// </summary>
    public void Die()
    {
        audioSource.PlayOneShot(hitSound);
        super = false;
        OnDied?.Invoke();
    }

    /// <summary>
    /// OnTriggerEnter2D to check for mushroom and invisible wall collision
    /// If we trigger a mushroom collider, we pick it up, change to super mode, and destroy the mushroom
    /// If we trigger an invisible wall we set the bool to true and the player won't move in that direction
    /// </summary>
    /// <param name="collision">Collider2D of the GameObject we triggered</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mushroom"))
        {
            audioSource.PlayOneShot(powerUpSound);
            super = true;
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            OnPowerUpPickedUp?.Invoke();
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("LeftInvisibleWall"))
        {
            onLeftInvisibleWall = true;
        }
    }

    /// <summary>
    /// OnTriggerExit2D to check when we are leaving the invisible wall
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftInvisibleWall"))
        {
            onLeftInvisibleWall = false;
        }
    }

    /// <summary>
    /// Coroutine for invulnerability
    /// The character's sprite will fade in and out for 4 iterations of 0.5 seconds each
    /// </summary>
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        int i = 0;
        while (i < 4)
        {
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            i++;
        }
        invulnerable = false;
    }
}
