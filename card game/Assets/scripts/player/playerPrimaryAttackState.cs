using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrimaryAttackState : playerState
{
    public playerPrimaryAttackState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
       
    }
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow=1;
    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCounter = 0; 
            
        }
        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        player.setVelocity(player.attackMovement[comboCounter] * attackDir, player.rb.velocity.y);
        player.animator.SetInteger("combocounter",comboCounter);
        generalStateTimer = 0.1f;
        player.busyFor(.2f);

    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (generalStateTimer < 0)
        {
            player.setVelocity(0, 0);
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
