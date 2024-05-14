using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Entity
{
    [Header("Attack details")]
    public float[] attackMovement ;
    public float counterAttackDuration=.2f;
    public bool playerIsBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 16f;
    public float jumpForce;
    public float DefaultGravity = 3.5f;
    [Header("Dash info")]
    public float dashSpeed = 30f;
    public float dashDuration = 0.2f;
    public float dashDir;

    #region skillInfo

    [Header("blackHole info")] public float flyTime;
    public float flySpeed;
    #endregion
    public GameObject sword{ get; private set; }
    public float swordReturnForce;


    #region States
    public playerStateMachine stateMachine { get; private set; }
    public playerIdleState idleState { get; private set; }
    public playerMoveState moveState { get; private set; }

    public playerAirState airState { get; private set; }
    public playerJumpState jumpState { get; private set; }

    public playerDashState dashState { get; private set; }

    public playerWallslideState wallslideState { get; private set; }

    public playerWalljumpState walljumpState { get; private set; }

    public playerPrimaryAttackState primaryAttackState { get; private set; }
    public playerCounterAttackState playerCounterAttackState {  get; private set; }

    public playerSuccessCounterState playerSuccessCounterState { get; private set; }
    
    public playerAimSwordState playerAimSwordState{ get; private set; }
    
    public playerCatchSwordState playerCatchSwordState{ get; private set; }
    
    public playerBlackHoleState playerBlackHoleState { get; private set; }
    #endregion

    protected override void Awake()
    {   
        base.Awake();
        stateMachine = new playerStateMachine();
        idleState = new playerIdleState(stateMachine,this,"idle");
        moveState = new playerMoveState(stateMachine, this, "move");
        airState = new playerAirState(stateMachine, this, "jump");
        jumpState = new playerJumpState(stateMachine, this, "jump");
        dashState = new playerDashState(stateMachine, this, "dash");
        wallslideState = new playerWallslideState(stateMachine, this, "wallslide");
        walljumpState = new playerWalljumpState(stateMachine, this, "walljump");
        primaryAttackState = new playerPrimaryAttackState(stateMachine, this, "primaryattack");
         playerCounterAttackState = new playerCounterAttackState(stateMachine, this, "counterattack");
        playerSuccessCounterState = new playerSuccessCounterState(stateMachine, this, "successcounter");
        playerAimSwordState = new playerAimSwordState(stateMachine, this, "aimsword");
        playerCatchSwordState = new playerCatchSwordState(stateMachine, this, "catchsword");
        playerBlackHoleState= new playerBlackHoleState(stateMachine, this, "jump");




    }
    protected override void Start()
    {   
        base.Start();
        stateMachine.Initialized(idleState);
        

    }



    public void exitBlackHoleSkill()
    {
        stateMachine.ChangeState(airState);
    }

    public void assginSword(GameObject newsword)
    {
        sword = newsword;
    }

    public void deleteSword()
    {
        Destroy(sword);
    }
    public IEnumerator busyFor(float seconds)
    {   
        playerIsBusy = true;
        yield return new WaitForSeconds(seconds);
        playerIsBusy = false;
    }
    protected override void Update()
    {   
        base.Update();
        stateMachine.currentState.Update();

    }


    public void AnimationTrigger()=> stateMachine.currentState.AnimationFinishTrigger();



}
