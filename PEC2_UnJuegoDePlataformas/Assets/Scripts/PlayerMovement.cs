using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontal;
    private Vector3 initialPosition;
    private bool super;
    private bool invulnerable;

    [SerializeField] float velocity = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] Transform feet;
    [SerializeField] float groundedCheckRadius = 0.1f;
    [SerializeField] LayerMask floorMask;

    public bool IsSuper => super;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;
        super = false;
        invulnerable = false;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        rigidbody2d.velocity = new Vector2(horizontal * velocity, rigidbody2d.velocity.y);

        animator.SetBool("walking", horizontal != 0);

        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        else
        {
            animator.SetBool("jumping", !IsGrounded());
        }
    }

    private void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
        animator.SetBool("jumping", true);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, groundedCheckRadius, floorMask);
    }

    public void TakeDamage()
    {
        if (invulnerable)
            return;

        if(super)
        {
            super = false;
            transform.localScale = new Vector3(1, 1, 1);
            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        transform.position = initialPosition;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mushroom"))
        {
            super = true;
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }

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
