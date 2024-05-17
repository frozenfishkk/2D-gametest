using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour

{   
    #region event

    public System.Action onFlipped;
    

    #endregion
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
    public SpriteRenderer sr { get; private set; }
    #endregion
    #region AttackInfo
    public Transform attackCheck;
    public float attackCheckRadius;
    
    #endregion
    public entityFx damagedFX {  get; private set; }
    [Header("knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    public charaterStats charaterStats;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        damagedFX = GetComponentInChildren<entityFx>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        charaterStats = GetComponent<charaterStats>();
    }

    public void invis(bool isInvis)
    {
        if (isInvis)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
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
        if (onFlipped!=null)
        {
            onFlipped();
        }
        
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

    public virtual void damageEffect()
    {
        damagedFX.StartCoroutine("flashFX");
        StartCoroutine("hitKnockBack");
    }
    public void setVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
        flipController(xVelocity);
    }
    protected virtual IEnumerator hitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;

    }
}
