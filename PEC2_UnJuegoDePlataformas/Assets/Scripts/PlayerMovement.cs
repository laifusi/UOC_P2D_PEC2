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

    [SerializeField] float velocity = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] Transform feet;
    [SerializeField] float groundedCheckRadius = 0.1f;
    [SerializeField] LayerMask floorMask;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;
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

    public void Die()
    {
        transform.position = initialPosition;
    }
}
