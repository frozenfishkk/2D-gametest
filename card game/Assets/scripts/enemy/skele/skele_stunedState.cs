using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele_stunedState : enemyState
{
    private skele skele;

public skele_stunedState(enemy enemyBase, enemyStateMachine stateMachine, string animBoolName,skele skele) : base(enemyBase, stateMachine, animBoolName)
    {
        this.skele = skele;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {   
        Debug.Log("stun");
        base.Enter();
        stateTimer = skele.stunTime;
        skele.damagedFX.InvokeRepeating("colorBlink", 0, .1f);


    }

    public override void Exit()
    {
        base.Exit();
        skele.damagedFX.Invoke("cancelBlink",0);
    }

    public override void Update()
    {
        base.Update();
        skele.setVelocity(0, 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(skele.battleState);
        }
    }
}
