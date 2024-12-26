using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    private bool isMoving;
    [SerializeField] private float walkSpeed;
    private float currentSpeed;
    public GameObject bodyObj;

    [Header("Grounding")]
    public float groundCheckDistance = 1.1f;
    public float wallCheckDistance = 0.55f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private bool isGrounded;
    private bool isWallLeft;
    private bool isWallRight;

    [Header("Jump")]
    [SerializeField] private float jumpingPower;
    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public int jumpMax;
    private int jumpLeft;

    [Header("Others")]
    [SerializeField] private Rigidbody2D rb;
    public bool lookRight = true;

    void Update()
    {
        isGrounded = CheckGrounded();
        isWallLeft = CheckWallLeft();
        isWallRight = CheckWallRight();
        HoldTheWall();

        Movement();
        CheckJump();

        LookAtMouse();
    }

    void CalculateSpeed()
    {
        currentSpeed = walkSpeed;
    }

    void Movement()
    {
        CalculateSpeed();
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Left]) && !isWallLeft)
        {
            isMoving = true;
            transform.Translate(-1 * currentSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Down]) && !isGrounded)
        {
            transform.Translate(0, -2 * currentSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Right]) && !isWallRight)
        {
            isMoving = true;
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
        }
        if (!Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Left]) && !Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Right]))
        {
            isMoving = false;
        }
    }
    bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            ResetJumps();
            return true;
        }
        return false;
    }
    bool CheckWallLeft()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);

        if (hitLeft.collider != null)
        {
            ResetJumps();
            return true;
        }
        return false;
    }
    bool CheckWallRight()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);

        if (hitRight.collider != null)
        {
            ResetJumps();
            return true;
        }
        return false;
    }
    void CheckJump()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Jump]))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if ((coyoteTimeCounter > 0f || jumpLeft > 0) && jumpBufferCounter > 0f && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }
    [Obsolete]
    IEnumerator Jump()
    {
        jumpLeft--;
        rb.velocity = Vector2.zero;
        isJumping = true;
        isGrounded = false;

        rb.AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;

        yield return new WaitForSeconds(0.3f);
        isJumping = false;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
    }
    private void ResetJumps()
    {
        jumpLeft = jumpMax;
    }
    private void HoldTheWall()
    {
        if (!isJumping
            && ((isWallLeft && Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Left]))
            || (isWallRight && Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Right]))))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < transform.position.x)
        {
            lookRight = false;
            Flip(false);
        }
        else
        {
            lookRight = true;
            Flip(true);
        }
    }

    void Flip(bool lookRight)
    {
        Vector3 scale = bodyObj.transform.localScale;
        scale.x = lookRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        bodyObj.transform.localScale = scale;
    }
}
