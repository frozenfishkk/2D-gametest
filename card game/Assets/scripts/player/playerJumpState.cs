using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJumpState : playerState
{
    public playerJumpState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            player.setVelocity(player.moveSpeed * xInput * 0.6f, player.rb.velocity.y);
        }
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
        }
        if (player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

    }

    // Start is called before the first frame update

}
