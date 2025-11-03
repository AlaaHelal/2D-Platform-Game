using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int health =100;
    private float moveSpeed = 5.0f;
    private float jumpForce = 10.0f;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public int extraJumpsCount =1;
    private int extraJumpsLeft;
    public int coinsCollected =0;

    private SpriteRenderer spriteRenderer;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rbPlayer;
    private Animator animator;
    public Image healthBar;
    private Vector2 platformVelocity;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            if (isGrounded )
            {
                // Apply jump force
                rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce); 
            }
            else if (extraJumpsLeft > 0)
            {
                // Apply jump force for double jump
                rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce);

                // Decrease extra jumps left if not grounded
                extraJumpsLeft--;
            }
                
        }

        // Update animations
        SetAnimations(horizontalInput);

        // Update health bar
        healthBar.fillAmount = health / 100f;

        
       
    }

    // Ground check in FixedUpdate for consistent physics checks
    void FixedUpdate()
    {
        /* Check if the player is grounded
         Using OverlapCircle for ground detection
         This checks if there are any colliders overlapping with the circle at groundCheck position
         with the specified radius and ground layer mask 
         and sets isGrounded accordingly*/
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Apply platform velocity if on a moving platform
        //if (platformVelocity != Vector2.zero)
        //{
        //    rbPlayer.linearVelocity += platformVelocity;
        //}
    }

    public void SetAnimations(float moveInput)
    {

        animator.SetFloat("speed", Math.Abs(moveInput));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rbPlayer.linearVelocity.y);
        //if (isGrounded)
        //{
        //    if(moveInput == 0)
        //    {
        //        // Set idle animation
        //        animator.Play("Player_Idle");
        //    }
        //    else
        //    {
        //        // Set running animation
        //        animator.Play("Player_Run");
        //    }
        //}
        //else
        //{
        //    if(rbPlayer.linearVelocityY > 0)
        //        {
        //        // Set jumping animation
        //        animator.Play("Player_Jump");
        //    }
        //    else
        //    {
        //        // Set falling animation
        //        animator.Play("Player_Fall");
        //    }
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Damage")
        {
            // Reduce health and apply knockback
            health -= 25;
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce);

            // Start damage blink effect
            StartCoroutine(BlinkDamage());

            // Check for death
            if (health <= 0)
            {
                Die();
            }
        }

        //else if (collision.gameObject.CompareTag("MovingPlatform"))
        //{
        //    foreach (var contact in collision.contacts)
        //    {
        //        if (contact.normal.y > 0.5f) // Check if the contact normal indicates standing on top
        //        {
        //            platformVelocity=collision.gameObject.GetComponent<MovingPlatform>().platformVelocity;
        //        }
        //    }
        //}

        //else if (collision.gameObject.CompareTag("Ground"))
        //{
        //    foreach (var contact in collision.contacts)
        //    {
        //        if (contact.normal.y > 0.5f) // Check if the contact normal indicates standing on top
        //        {
        //            isGrounded = true;
        //            return;
        //        }
        //    }
        //}
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("MovingPlatform"))
    //    {
    //        platformVelocity = Vector2.zero;
    //    }

    //    else if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    IEnumerator BlinkDamage() {
        // Blink red to indicate damage
        spriteRenderer.color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        // Revert to original color
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        // Reload the current scene on death
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
