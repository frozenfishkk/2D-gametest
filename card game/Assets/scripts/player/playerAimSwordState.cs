using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimSwordState : playerState
{
    public playerAimSwordState(playerStateMachine stateMachine, player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        skillManager.instance.throwSkill.createDots();
        skillManager.instance.throwSkill.changeSwordTypeRB(skillManager.instance.throwSkill.SwordType);
        
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(0,0);
        skillManager.instance.throwSkill.generateDots(true);
        if (skillManager.instance.throwSkill.aimDirection().x>0 &&player.facingDir <0)
        {
            player.Flip();
        }
        else if (skillManager.instance.throwSkill.aimDirection().x<0 &&player.facingDir >0)
        {
            player.Flip();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {   
            
            stateMachine.ChangeState(player.idleState);
            
        }
    }

    public override void Exit()
    {
        base.Exit();
        skillManager.instance.throwSkill.generateDots(false);
        player.StartCoroutine("busyFor", 0.2f);
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
