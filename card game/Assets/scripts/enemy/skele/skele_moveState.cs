using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_moveState : skele_groundState
{
    public skele_moveState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName, skele enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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
        skele.setVelocity(skele.moveSpeed * skele.facingDir, skele.rb.velocity.y);
        if (skele.isWalled||!skele.isGrounded)
        {
            
            stateMachine.ChangeState(skele.idleState);
            skele.Flip();
        }
    }
}
 
