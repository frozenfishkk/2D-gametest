using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : charaterStats
{
    protected enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<enemy>();
    }

    public override void takeDamage(float _damage,magicalDmgStat magicalDmg)
    {
        base.takeDamage(_damage,magicalDmg);
        enemy.damageEffect();
    }

    protected override void Update()
    {
        base.Update();
       
    }

    public override void die()
    {
        base.die();
        enemy.enemyDead();
    }
}
