using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float maxJumpTime = 0.3f;
    public LayerMask groundLayer;

    private float jumpTimeCounter;
    private bool isJumping;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Jump();

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 newVector = new Vector2(moveHorizontal * moveSpeed, rb.linearVelocity.y);

        rb.linearVelocity = newVector;
    }

    void Jump()
    {
        isGrounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            Debug.Log("Jump started");
        }

        // If the jump button is held and the player is still within the max jump time, continue jumping
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(Vector2.up * (jumpSpeed * Time.deltaTime), ForceMode2D.Force);
                jumpTimeCounter -= Time.deltaTime;
                Debug.Log("Jumping, time left: " + jumpTimeCounter);
            }
        }
        else
        {
            isJumping = false;
        }

        // If the player releases the jump button early, cut the jump short
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Jump button released");
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
                Debug.Log("Jump cut");
            }
            isJumping = false;
        }

        if (isGrounded)
        {
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        return boxCollider.IsTouchingLayers(groundLayer);
    }

    public void Die()
    {
        Debug.Log("Player has died!");

        transform.position = new Vector3(-19.32f, 7.06f, 0);
    }
}
