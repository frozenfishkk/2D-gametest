using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneController : MonoBehaviour
{
    private SpriteRenderer renderer;
    private float cloneTimer;
    private float cloneDuration;
    private float loseColorSpeed;
    private Animator animator;
    [SerializeField]private Transform attackCheck;
    private Rigidbody2D rb;
    private int facingDir  = 1;
    private bool facingRight = true;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private  void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir *= -1;
        facingRight = !facingRight;
    }

    public void setupClone(Transform newtransform,float _cloneDuration,float _loseColorSpeed,int _attacknumber)
    {
        cloneDuration = _cloneDuration;
        loseColorSpeed = _loseColorSpeed;
        cloneTimer = cloneDuration;
        transform.position = newtransform.position;
        animator.SetInteger("attacknumber",_attacknumber);
        rb.velocity = new Vector2(0.1f * playerManager.instance.player.facingDir, rb.velocity.y);
    }
    private  void flipController(float x)
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
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer<0)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - (Time.deltaTime * loseColorSpeed));
        }

        if (renderer.color.a<=0)
        {
            Destroy(gameObject);
        }
        flipController(rb.velocity.x);
    }
    
    private void AnimationTrigger()
    {
        
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, playerManager.instance.player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                hit.GetComponent<enemy>().damage();
            }
        }
    }
}
