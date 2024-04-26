using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyState 
{
    protected enemy enemyBase { get; private set; }
    protected enemyStateMachine stateMachine { get; private set; }

    protected bool triggerCalled;
    private string animBoolName;
    public float battleTime;
    protected float stateTimer;

    public enemyState(enemy enemyBase, enemyStateMachine stateMachine,  string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter() 
    {
        enemyBase.animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Update() {
    stateTimer -= Time.deltaTime;

    }

    public virtual void Exit() { 
    
        triggerCalled = true;
        enemyBase.animator.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
