using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlackHoleState : playerState
{
    private float flyTime;
    private bool skillUsed ;
    private float flySpeed;
    public playerBlackHoleState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        flyTime = player.flyTime;
        flySpeed = player.flySpeed;
        stateTimer = flyTime;
        skillUsed = false;
        player.rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer>0)
        {
            player.setVelocity(0,flySpeed);
        }

        if (stateTimer<0)
        {   
            player.setVelocity(0,.1f);
            if (!skillUsed)
            {   
                Debug.Log("used");
                skillUsed = true;
                skillManager.instance.blackHoleSkill.useSkill();
                
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = player.DefaultGravity;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
