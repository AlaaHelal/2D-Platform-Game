using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float jumpForce = 10.0f;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public int extraJumpsCount =1;
    private int extraJumpsLeft;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rbPlayer;
    private Animator animator;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Initialize extra jumps
        extraJumpsLeft = extraJumpsCount;
    }

    
    void Update()
    {
        // Handle horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbPlayer.linearVelocity.y);

        // Ground Check and Reset Extra Jumps Left
        if (isGrounded)
        {
            extraJumpsLeft = extraJumpsCount;
        }

        //Double Jump Logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || extraJumpsLeft > 0)
            {
                Debug.Log($"{extraJumpsLeft}");
                rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce);
                Debug.Log($"{extraJumpsLeft}");
                extraJumpsLeft--;
                Debug.Log($"{extraJumpsLeft}");
            }
        }
        
            

        // Update animations
        SetAnimations(horizontalInput);
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void SetAnimations(float moveInput)
    {
        if (isGrounded)
        {
            if(moveInput == 0)
            {
                // Set idle animation
                animator.Play("Player_Idle");
            }
            else
            {
                // Set running animation
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rbPlayer.linearVelocityY > 0)
                {
                // Set jumping animation
                animator.Play("Player_Jump");
            }
            else
            {
                // Set falling animation
                animator.Play("Player_Fall");
            }
        }

    }
}
