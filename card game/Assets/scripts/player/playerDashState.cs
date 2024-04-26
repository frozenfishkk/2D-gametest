using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDashState : playerState
{
    public float timer;
    public playerDashState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        timer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.setVelocity(0, player.rb.velocity.y);
        
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(player.dashSpeed * player.dashDir, 0);
        timer -= Time.deltaTime;
        if (!player.isGrounded && player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
        }
        if (timer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        
    }


}
