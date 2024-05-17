using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAnimationTriggers : MonoBehaviour
{
    private player player => GetComponentInParent<player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                enemyStats targetStats = hit.GetComponent<enemyStats>();
                player.playerStats.doDamage(targetStats);
            }
        }
    }

    private void throwTrigger()
    {
        if (player.sword==null)
        {
            skillManager.instance.throwSkill.createSword();
        }
        
    }
}
