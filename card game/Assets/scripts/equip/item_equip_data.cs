using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum equipmentType
{
   primaryWeapon,
   secondaryWeapon,
   armor,
   helmet,
   bottom,
}
[CreateAssetMenu(fileName = "itemData", menuName = "Data/equipment")]
public class item_equip_data : itemData
{
    public itemEffect[] effects;
    public equipmentType equipmentType;
    [Header("major ints")]
    public int strength; //1 for 1 dmg and 1% crit.dmg
    public int agility;//1 for 1 evasion and 1% crit chance
    public int intelligence;// 1 for 1 magic dmg and 1% magic resist
    public int vitality;// 1 for 5hp and stamina for ?
    [Header("defensive ints")]
    public int maxHP;
    public int armour;
    public int magicResistance;
    public int evasion;
    public int fireResistance;
    public int iceResistance;
    public int lightningResistance;
    [Header("attack stats")]
    public int damage;
    public int critChance;
    public int critDamage;
    [Header("magic damage")] 
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;
    public int magicDamage;

    public void addEffect()
    {
        foreach (var effect in effects)
        {
            effect.enableEffect();
        }
    }
    public void addModifier()
    {
        playerStats playerStats = playerManager.instance.player.GetComponent<playerStats>();
        playerStats.strength.addModifer(strength);
        playerStats.agility.addModifer(agility);
        //添加所有属性
        playerStats.intelligence.addModifer(intelligence);
        playerStats.vitality.addModifer(vitality);
        playerStats.maxHP.addModifer(maxHP);
        playerStats.armour.addModifer(armour);
        playerStats.magicResistance.addModifer(magicResistance);
        playerStats.evasion.addModifer(evasion);
        playerStats.fireResistance.addModifer(fireResistance);
        playerStats.iceResistance.addModifer(iceResistance);
        playerStats.lightningResistance.addModifer(lightningResistance);
        playerStats.damage.addModifer(damage);
        playerStats.critChance.addModifer(critChance);
        playerStats.critDamage.addModifer(critDamage);
        playerStats.fireDamage.addModifer(fireDamage);
        playerStats.iceDamage.addModifer(iceDamage);
        playerStats.lightningDamage.addModifer(lightningDamage);
        playerStats.magicDamage.addModifer(magicDamage);
        
        
    }

    public void removeModifier()
    {
        
        playerStats playerStats = playerManager.instance.player.GetComponent<playerStats>();
        playerStats.strength.removeModifer(strength);
        playerStats.agility.removeModifer(agility);
        playerStats.intelligence.removeModifer(intelligence);
        playerStats.vitality.removeModifer(vitality);
        playerStats.maxHP.removeModifer(maxHP);
        playerStats.armour.removeModifer(armour);
        playerStats.magicResistance.removeModifer(magicResistance);
        playerStats.evasion.removeModifer(evasion);
        playerStats.fireResistance.removeModifer(fireResistance);
        playerStats.iceResistance.removeModifer(iceResistance);
        playerStats.lightningResistance.removeModifer(lightningResistance);
        playerStats.damage.removeModifer(damage);
        playerStats.critChance.removeModifer(critChance);
        playerStats.critDamage.removeModifer(critDamage);
        playerStats.fireDamage.removeModifer(fireDamage);
        playerStats.iceDamage.removeModifer(iceDamage);
        playerStats.lightningDamage.removeModifer(lightningDamage);
        playerStats.magicDamage.removeModifer(magicDamage);
        
    }
}
