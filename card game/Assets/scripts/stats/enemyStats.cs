using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : charaterStats
{
    protected enemy enemy;
    public int enemyLevel=1;
    public string enemyID;
    [UnityEngine.Range(0f,1f)]
    [SerializeField] public float percentageModifier=0.1f;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<enemy>();
        levelStatModify();
        maxHP.getFinalValue();
    }

    public void levelStatModify()
    {
        levelModify(strength);
        levelModify(agility);
        levelModify(intelligence);
        levelModify(vitality);
        levelModify(armour);
        levelModify(magicResistance);
        levelModify(evasion);
        levelModify(fireResistance);
        levelModify(iceResistance);
        levelModify(lightningResistance);
        levelModify(damage);
        levelModify(critChance);
        levelModify(critDamage);
        levelModify(fireDamage);
        levelModify(iceDamage);
        levelModify(lightningDamage);
        levelModify(magicDamage);
        levelModify(maxHP);
    }
    public void levelModify(stat _stat)
    {   
        float modify = _stat.getFinalValue()*percentageModifier;
        for (int i = 1; i < enemyLevel; i++)
        {
            _stat.addModifer(modify);
        }
    }
    public override void takeDamage(float _damage,stat fireDMG,stat iceDMG,stat lightningDMG,stat magicalDmg)
    {
        base.takeDamage(_damage,fireDMG,iceDMG, lightningDMG, magicalDmg);
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
