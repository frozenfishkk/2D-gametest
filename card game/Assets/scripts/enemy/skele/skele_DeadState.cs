using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_DeadState : enemyState
{
    private skele skele;
    public skele_DeadState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName,skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.skele = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        skele.setVelocity(0,0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
