using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class skele_AnimationTriggers : MonoBehaviour
{
    private skele ske => GetComponentInParent<skele>();
    private void AnimationTrigger()
    {
        ske.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ske.attackCheck.position, ske.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<player>() != null)
            {
                hit.GetComponent<player>().damage();
            }

        }
    }
    protected void openCounterWindow()=>ske.openCounterAttackWindow();
    protected void closeCounterWindow()=>ske.closeCounterAttackWindow();
}