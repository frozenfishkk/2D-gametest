using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    private Animator animator;
    private float xInput;
    private int facingDirection = 1;
    private bool facingRight = true;
    [Header("DashInfo")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashColddown;
    private float dashColddownTimer;
    [Header("CollisionInfo")]
    [SerializeField] private float groundCheckDistance;
    private bool isOnGround;
    [SerializeField] private LayerMask Ground;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        moveSpeed = 8;
        jumpPower = 12;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        getInput();
        AnimatorContorllers();
        flipController();
        CollisionChecks();
        dashTime -=Time.deltaTime;
        dashColddownTimer -=Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashColddownTimer < 0) { dashTime = dashDuration; }

    }

    private void CollisionChecks()
    {
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, Ground);
    }

    private void getInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }

    private void Move()
    {   if (dashTime > 0 )
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
            dashColddownTimer = dashColddown;
        }
    else { rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y); }
        
    }

    private void jump()
    {   if (isOnGround) { rb.velocity = new Vector2(rb.velocity.x, jumpPower); }
        
    }

    private void AnimatorContorllers()
    {
        bool isMoving = rb.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGround", isOnGround);
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void flipController()
    {
        if(rb.velocity.x > 0 && !facingRight) { Flip(); }
        else if (rb.velocity.x<0 && facingRight) { Flip(); }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y - groundCheckDistance));
        
    }
}
