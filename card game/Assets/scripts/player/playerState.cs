using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState 
{
    protected playerStateMachine stateMachine;
    protected player player;
    protected float xInput;
    protected float yInput;
    private string animBoolName;
    public float stateTimer;
    public float generalStateTimer;
    protected bool triggerCalled;

    public playerState(playerStateMachine stateMachine, player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter()
    {
        
        player.animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        player.dashTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", player.rb.velocity.y);
        stateTimer-= Time.deltaTime;
        generalStateTimer-= Time.deltaTime;
        checkDash();
        

    }


    private void checkDash()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.dashTimer<0)
        {
            player.dashTimer = player.dashColdDown;
            player.dashDir = Input.GetAxisRaw("Horizontal");
            if (player.dashDir == 0) { player.dashDir = player.facingDir; }
            stateMachine.ChangeState(player.dashState);
        }
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
