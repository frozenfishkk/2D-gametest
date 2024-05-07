using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_groundState :enemyState
{
   
    protected skele skele;
    protected Transform player;

    public skele_groundState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName, skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.skele = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = playerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (skele.playerDetected() || Vector2.Distance(player.transform.position, skele.transform.position) <2)
        {
            stateMachine.ChangeState(skele.battleState);
        }
    }
}
