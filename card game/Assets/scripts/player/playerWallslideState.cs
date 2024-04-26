using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWallslideState :playerState
{
    public float wallslideDuration = 0.1f;
    public playerWallslideState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.walljumpState);
            return;
        }
        if(xInput !=0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
            
        }
        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
