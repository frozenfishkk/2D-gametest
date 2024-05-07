using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashSkill : skill
{   
    public override bool canUseSkill()
    {
        return base.canUseSkill();
    }

    public override void useSkill()
    {
        base.useSkill();
        
        Debug.Log("dash clone ");
    }

    protected override void Update()
    {
        base.Update();
    }
}
