using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_attackState : enemyState
{
    protected skele skele;
    public skele_attackState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName, skele enemy) : base(enemyBase, stateMachine, animBoolName)
    { skele = enemy; }

    public override void Enter()
    {
        base.Enter();
        skele.attackTimer = skele.attackColdDown;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(skele.battleState);
        }
    }
}

