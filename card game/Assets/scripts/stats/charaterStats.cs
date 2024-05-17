using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charaterStats : MonoBehaviour
{
    [Header("major stats")]
    public stat strength; //1 for 1 dmg and 1% crit.dmg
    public stat agility;//1 for 1 evasion and 1% crit chance
    public stat intelligence;// 1 for 1 magic dmg and 1% magic resist
    public stat vitality;// 1 for 5hp and stamina for ?
    [Header("defensive stats")]
    public stat maxHP;
    public stat armour;
    public stat magicResistance;
    public stat evasion;
    public stat fireResistance;
    public stat iceResistance;
    public stat lightningResistance;
    [Header("attack stats")]
    public stat damage;

    public stat critChance;
    public stat critDamage;
    [Header("magic damage")] 
    public stat fireDamage;
    public stat iceDamage;
    public stat lightningDamage;
    private magicalDmgStat totalMagicDmg;
    public float totalInvasion;
    public float totalCritChance;
    public float currentHP;

    public float totalCritDmg;
    
[Header("magical effect")]
    public bool isIgnite;

    public bool isChilled;

    public bool isShocked;
    private float effectTimer;
    [SerializeField]private float effectDuration;

    #region event

    public  System.Action onHealthChanged;

    #endregion
    // Start is called before the first frame update
    public virtual void doDamage(charaterStats _targetStats)
    {   
        
        float totalDamage =damage.getFinalValue();
        magicDamageEffect(totalMagicDmg);
        _targetStats.takeDamage(totalDamage,totalMagicDmg);
        _targetStats.applyMagicalEffect(isIgnite,isChilled,isShocked);
    }

    public virtual void takeDamage(float _damage,magicalDmgStat magicalDmg)
    {
        if (canEvasion())
            return;
        if (canCrit())
        {
            _damage = _damage*critDamage.getFinalValue()/100;
        }

        float magicalDMG = magicalDamageReduction(magicalDmg);
        _damage = physicalDamageReduction(_damage);
        currentHP -= _damage;
        currentHP -= magicalDMG;
        if (onHealthChanged!=null){onHealthChanged();}
        if (currentHP<=0)
        {
            die();
        }
    }

    public virtual void takeDamageWithOutEffect(float _damage)
    {
        currentHP-=_damage;
        if (onHealthChanged!=null){onHealthChanged();}
        
    }

    private void clearEffect()
    {
        isIgnite = false;
        isChilled = false;
        isShocked = false;
        effectTimer = Mathf.Infinity;
    }
    private void magicDamageEffect(magicalDmgStat magicalDmg)
    {
        if (magicalDmg.fireDmg>0 && magicalDmg.iceDmg<=0 && magicalDmg.lightningDmg<=0)
        {
            isIgnite = true;
        }
        else if (magicalDmg.iceDmg > 0 && magicalDmg.fireDmg <= 0 && magicalDmg.lightningDmg <= 0)
        {
            isChilled = true;
        }
        else if (magicalDmg.lightningDmg > 0 && magicalDmg.iceDmg <= 0 && magicalDmg.fireDmg <= 0)
        {
            isShocked = true;
        }
        else
        {
            float r = Random.Range(0, 1);
            if (r<0.33)
            {
                isIgnite = true;
            }
            else if (r<0.66)
            {
                isChilled = true;
            }
            else
            {
                isShocked = true;
            }
        }
    }
    
    public void applyMagicalEffect(bool _isIgnit,bool _isChilled,bool _isShocked)
    {
        if (isIgnite|| isChilled|| isShocked)
        {
            return;
        }

        isIgnite = _isIgnit;
        isChilled = _isChilled;
        isShocked = _isShocked;
        effectTimer = effectDuration;
        if (isIgnite)
        {
            Debug.Log("ignite");
        }
        if (isChilled)
        {
            Debug.Log("chilled");
        }
        if (isShocked)
        {
            Debug.Log("shocked");
        }
    }

    public  float magicalDamageReduction(magicalDmgStat magicalDmgStat)
    {
        float fire = Mathf.Clamp(magicalDmgStat.fireDmg -fireResistance.getFinalValue(),0,magicalDmgStat.fireDmg);
        float ice = Mathf.Clamp(magicalDmgStat.iceDmg -iceResistance.getFinalValue(),0,magicalDmgStat.iceDmg);
        float lightning = Mathf.Clamp(magicalDmgStat.lightningDmg -lightningResistance.getFinalValue(),0,magicalDmgStat.lightningDmg);
        float addon = Mathf.Clamp(magicalDmgStat.dmgAddon - magicResistance.getFinalValue(),0,magicalDmgStat.dmgAddon);
        return fire + ice + lightning+addon;
    }
    public float physicalDamageReduction(float _dmg)
    {
        _dmg -= armour.getFinalValue();
        if (_dmg<0)
        {
            _dmg = 0;
        }
        return _dmg;
    }
    private bool canEvasion()
    {
        if (UnityEngine.Random.Range(0,100)<totalInvasion)
        {
            Debug.Log("evasion");
            return true;
        }

        return false;
    }

    private bool canCrit()
    {
        if (Random.Range(0,100)<totalCritChance)
        {
            Debug.Log("crit hit");
            return true;
            
        }

        return false;
    }
    protected virtual void Start()
    {
        currentHP = maxHP.getFinalValue();
        totalInvasion = getTotalEvasion(evasion.getFinalValue());
        totalCritChance = getTotalCritChance(critChance.getFinalValue());
        totalCritDmg = getTotalCritDmg(critDamage.getFinalValue());
        totalMagicDmg = new magicalDmgStat(fireDamage, iceDamage, lightningDamage,intelligence);
        critDamage.setDefaultValue(150);
        if (onHealthChanged !=null)
        {
            onHealthChanged();
        }
       


    }



    private float getTotalEvasion(float evasion)
    {
        return evasion+agility.getFinalValue();
    }

    private float getTotalCritChance(float critChance)
    {
        return critChance;
    }
    private float getTotalCritDmg(float critDmg)
    {
        return critDmg+strength.getFinalValue();
    }


    public virtual void die()
    {
        
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        effectTimer -= Time.deltaTime;
        if (effectTimer<0)
        {
            clearEffect();
        }
    }
}
