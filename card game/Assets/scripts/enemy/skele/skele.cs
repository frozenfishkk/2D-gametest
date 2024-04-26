using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele : enemy
{
    #region state
    public skele_idleState idleState { get; private set; }
    public skele_moveState moveState { get; private set; }

    public skele_battleState battleState { get; private set; }

    public skele_attackState attackState { get; private set; }

    public skele_stunedState stunedState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        idleState = new skele_idleState(this,stateMachine,"idle",this);
        moveState = new skele_moveState(this, stateMachine, "move", this);
        battleState = new skele_battleState(this, stateMachine, "move", this);
        attackState = new skele_attackState(this, stateMachine, "attack", this);
        stunedState = new skele_stunedState(this, stateMachine, "stun", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
