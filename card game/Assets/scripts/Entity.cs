using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour

{
    [Header("Collision info")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayerMask;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask wallLayerMask;
    #region Compenents
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    #region AttackInfo
    public Transform attackCheck;
    public float attackCheckRadius;
    #endregion
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    protected virtual void Update()
    {

    }

    #region CollisionCheck
    public virtual bool isGrounded => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
    public virtual bool isWalled => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayerMask);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Flip

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    public virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir *= -1;
        facingRight = !facingRight;
    }

    public virtual void flipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        else if (x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    public virtual void damage()
    {

    }
    public void setVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        flipController(xVelocity);
    }

}
