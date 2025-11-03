using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private SpriteRenderer sr; // New variable for the SpriteRenderer
    private bool isGrounded;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player! Movement script requires it.");
        }

        // Add a check for the SpriteRenderer too
        if (sr == null)
        {
            Debug.LogWarning("SpriteRenderer component not found. Sprite flipping will not work.");
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- Jumping Logic ---
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // --- Sprite Flipping Logic (New) ---
        FlipSprite(moveInput);
    }

    void FixedUpdate()
    {
        // Using the original direct velocity method for simplicity here:
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // If you used the AddForce method I suggested before, that goes here instead.
    }

    // --- New dedicated method for flipping the sprite ---
    void FlipSprite(float input)
    {
        // Only attempt to flip if the SpriteRenderer exists and the player is moving horizontally
        if (sr != null && Mathf.Abs(input) > 0.01f)
        {
            // If moving right (positive input), ensure flipX is false
            if (input > 0)
            {
                sr.flipX = false;
            }
            // If moving left (negative input), ensure flipX is true
            else if (input < 0)
            {
                sr.flipX = true;
            }
        }
    }
}