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

    [Header("Wall Jump System")]
    public Transform WallCheck;
    bool isWallTouch;
    bool isSliding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 WallJumpForce;
    bool wallJumping;
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

        
        isWallTouch = Physics2D.OverlapBox(WallCheck.position, new Vector2(0.08f, 0.5f), 0, groundLayer);

        if (isWallTouch && !isGrounded() && horizontal != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (wallJumping)
        {
            // Tentukan arah dorongan berdasarkan arah dinding
            float wallJumpDirection = isFacingRight ? -1 : 1;

            // Berikan dorongan kecepatan saat wall jump
            rb.velocity = new Vector2(wallJumpDirection * WallJumpForce.x, WallJumpForce.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

    }
    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded())
            {
                // Jika berada di tanah, lakukan lompatan biasa
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsRemaining = maxJumps - 1; // Mengatur ulang sisa lompatan
            }
            else if (jumpsRemaining > 0)
            {
                // Jika sisa lompatan masih ada, lakukan lompatan ganda
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsRemaining--;
            }
            else if (isSliding)
            {
                // Jika dalam keadaan sliding di dinding, lakukan wall jump
                wallJumping = true;
                Invoke("StopWallJump", wallJumpDuration);
            }
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            // Mengurangi kecepatan vertikal saat lompat dibatalkan
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

    void StopWallJump()
    {
        wallJumping = false;
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
        tr.emitting = true;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
