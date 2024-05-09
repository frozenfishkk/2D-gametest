using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCatchSwordState : playerState
{
    private Transform sword;
    private float betweenSwordAndPlayer;
    public playerCatchSwordState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }
    
    public override void Enter()
    {   
        base.Enter();
        sword= player.sword.transform;
        betweenSwordAndPlayer = sword.position.x - player.transform.position.x;
        if (sword.position.x>player.transform.position.x &&player.facingDir <0)
        {
            player.Flip();
        }
        else if (sword.position.x<player.transform.position.x &&player.facingDir >0)
        {
            player.Flip();
        }
    }

    public override void Update()
    {
        base.Update();
        player.rb.velocity = new Vector2(player.swordReturnForce* (betweenSwordAndPlayer)*-1, 0);
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("busyFor", 0.1f);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
