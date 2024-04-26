using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveState : playerGroundState
{
    public playerMoveState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        player.setVelocity(xInput*player.moveSpeed, player.rb.velocity.y);
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
