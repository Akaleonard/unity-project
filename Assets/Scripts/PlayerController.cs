using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    public LayerMask groundLayer;

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
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return boxCollider.IsTouchingLayers(groundLayer);
    }

    public void Die()
    {
        Debug.Log("Player has died!");

        transform.position = new Vector3(0, 0, 0);
    }
}
