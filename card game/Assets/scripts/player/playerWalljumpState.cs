using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWalljumpState : playerState
{
    public playerWalljumpState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.1f;
        player.setVelocity(player.moveSpeed*1.1f * -player.facingDir, player.jumpForce*1.1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (stateTimer < 0&&player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
            
        }
    }
}
