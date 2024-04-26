using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAirState : playerState
{
    public playerAirState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
        }
        if (player.isGrounded)
        {   
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)
        {
            player.setVelocity(player.moveSpeed* xInput*0.6f,player.rb.velocity.y);
        }
    }
}
