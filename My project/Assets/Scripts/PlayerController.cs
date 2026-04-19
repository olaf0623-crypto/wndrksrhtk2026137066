using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Playercontroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float boostedJumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;
    private bool Invincible = false;
    private float originalJumpForce;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();

        originalJumpForce = jumpForce;
    }


    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);


        if (Invincible)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(5, 5, 5);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-5, 5, 5);

        }
        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(5, 5, 5);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-5, 5, 5);
        }


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }


    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            if (Invincible)
                return;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<Level>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy"))
        {
            if (Invincible)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("InvincibleItem"))
        {
            Invincible = true;
            Invoke(nameof(ResetInvincible), 2f);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("boostedJumpItem"))
        {
            jumpForce = boostedJumpForce;
            Invoke(nameof(ResetJumpForce), 5f);
            Destroy(collision.gameObject);
        }



    }
    
    void ResetInvincible()
    {
        Invincible = false;
    }

    void ResetJumpForce()
    {
        jumpForce = originalJumpForce;
    }
}

