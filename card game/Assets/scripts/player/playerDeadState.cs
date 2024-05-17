using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeadState : playerState
{
    public playerDeadState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
    }

    public override void Exit()
    {   
        base.Exit();
    }
}
