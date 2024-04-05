using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    public float speed = 5f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private int jumpsRemaining;
    public int maxJumps = 2; // Adjust this for double jump

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }
    private void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }else if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && (isGrounded() || jumpsRemaining > 0))
        {
            if (jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsRemaining--;
            }
            else if (jumpsRemaining == 0 && isGrounded()) // Reset jumps if grounded
            {
                jumpsRemaining = maxJumps - 1; // -1 because the current jump is counted as well
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
