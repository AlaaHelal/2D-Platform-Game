using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float jumpForce = 10.0f;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rbPlayer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbPlayer.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbPlayer.linearVelocity =new Vector2(rbPlayer.linearVelocity.x, jumpForce);
        }
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
