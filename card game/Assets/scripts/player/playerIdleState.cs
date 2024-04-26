using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerIdleState : playerGroundState
{
    public playerIdleState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(xInput);
        if(xInput != 0 && !player.playerIsBusy) 
        {
            stateMachine.ChangeState(player.moveState);

        }
    }
}
