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

    private SpriteRenderer spriteRenderer;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rbPlayer;
    private Animator animator;
    public Image healthBar;

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
            if (isGrounded || extraJumpsLeft > 0)
            {
                // Apply jump force
                rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce);
                // Decrease extra jumps left if not grounded
                extraJumpsLeft--;
               
            }
        }

        // Update animations
        SetAnimations(horizontalInput);

        healthBar.fillAmount = health / 100f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Damage" || collision.gameObject.tag == "Enemy")
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
    }

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
