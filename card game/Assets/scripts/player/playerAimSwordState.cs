using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimSwordState : playerState
{
    public playerAimSwordState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(0,0);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
