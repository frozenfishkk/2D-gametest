using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_battleState : enemyState
{
    private skele skele;
    private Transform player;
    private int moveDir;

    public skele_battleState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName,skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.skele = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("player").transform;
        stateTimer = battleTime;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        battleTime -= Time.deltaTime;
        skele.attackTimer-=Time.deltaTime;
        if (player.position.x > skele.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < skele.transform.position.x)
        {
            moveDir = -1;
        }
        skele.setVelocity(skele.moveSpeed * 1.2f * moveDir, skele.rb.velocity.y);
        if (skele.playerDetected())
        {
            if (skele.playerDetected().distance < skele.attackDistance && skele.attackTimer<0)
            {
                stateMachine.ChangeState(skele.attackState);
            }
        }
        else 
        {  
            if((battleTime < 0))
            stateMachine.ChangeState(skele.idleState);
        }
    }
}
