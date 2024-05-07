using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundState : playerState
{
    public playerGroundState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }
    private void checkAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
    }
    private void checkThrow()
    {
        if (Input.GetKeyDown(KeyCode.Q) )
        {
            
            stateMachine.ChangeState(player.playerAimSwordState);
        }
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
        checkAttack();
        checkThrow();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.playerCounterAttackState);
        }
        if (!player.isGrounded) {stateMachine.ChangeState(player.airState);}
        if (Input.GetKeyDown(KeyCode.Space)&& player.isGrounded)
         {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
