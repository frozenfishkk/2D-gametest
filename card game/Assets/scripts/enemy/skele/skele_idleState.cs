using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_idleState : skele_groundState

{
    public skele_idleState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName, skele enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = skele.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(skele.moveState);
        }
    }
}

