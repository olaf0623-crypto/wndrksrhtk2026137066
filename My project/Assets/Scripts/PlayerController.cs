using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Playercontroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float boostedJumpForce = 10f;
    public float boostedMoveSpeed = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;
    private bool Invincible = false;
    private float originalJumpForce;
    private float originalMoveSpeed;

    float score;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();

        originalJumpForce = jumpForce;

        originalMoveSpeed = moveSpeed;
    }


    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);


        if (Invincible)
        {
            if (moveInput > 0)
                transform.localScale = new Vector3(4, 4, 4);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-4, 4, 4);
        }
        else
        {
            if (moveInput > 0)
                transform.localScale = new Vector3(4, 4, 4);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-4, 4, 4);
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
            isGrounded = false;

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
            StageResultSaver.SaveStage(SceneManager.GetActiveScene().buildIndex, (int)score);
            HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
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
            score += 10f;
            score += collision.GetComponent<ItemObject>().GetPoint();
        }

        if (collision.CompareTag("boostedJumpItem"))
        {
            jumpForce = boostedJumpForce;
            Invoke(nameof(ResetJumpForce), 3f);
            Destroy(collision.gameObject);
            score += 10f;
            score += collision.GetComponent<ItemObject>().GetPoint();
        }

        if (collision.CompareTag("SpeedItem"))
        {
            moveSpeed = boostedMoveSpeed;
            Invoke(nameof(ResetMoveSpeed), 3f);
            Destroy(collision.gameObject);
            score += 10f;
            score += collision.GetComponent<ItemObject>().GetPoint();
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

    void ResetMoveSpeed()
    {
        moveSpeed = originalMoveSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            // 기존 점프 초기화
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // 위로 강하게 튐
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}

