using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCounterAttackState : playerState
{
    public playerCounterAttackState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {   
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.animator.SetBool("successcounter", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(0, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                
                if (hit.GetComponent<enemy>().canBeStunned()) {
                    player.animator.SetBool("successcounter", true);
                    stateTimer = 10;
                    hit.GetComponent<enemy>().closeCounterAttackWindow();
                    
                }
                
                
            }
        }
        if(stateTimer<0 || triggerCalled)
        {
            player.animator.SetBool("successcounter", false);
            stateMachine.ChangeState(player.idleState);

        }
    }


}
