using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private float horizontal;
    public float speed = 5f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private int jumpsRemaining;
    public int maxJumps = 2; // Adjust this for double jump

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }else if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
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

    public void dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash)
        {
            StartCoroutine(DashOn());
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

    private IEnumerator DashOn()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
