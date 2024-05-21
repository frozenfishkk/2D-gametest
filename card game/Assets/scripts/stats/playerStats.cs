using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : charaterStats
{   
    
    protected override void Start()
    {
        base.Start();
    }

    public override void takeDamage(float _damage,stat fireDMG,stat iceDMG,stat lightningDMG,stat magicalDmg)
    {
        base.takeDamage(_damage,fireDMG,iceDMG, lightningDMG, magicalDmg);
        playerManager.instance.player.damageEffect();
        
    }

    public override void die()
    {
        base.die();
        playerManager.instance.player.playerDead();
    }
}
