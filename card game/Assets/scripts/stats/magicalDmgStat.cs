using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class magicalDmgStat :stat
{


    public float fireDmg;
    public float iceDmg;
    public float lightningDmg;
    public float dmgAddon;

    public magicalDmgStat(stat _fireDmg, stat _iceDmg, stat _lightningDmg,stat _dmgAddon )//addon = inteli add
    {
        fireDmg = _fireDmg.getFinalValue();
        iceDmg = _iceDmg.getFinalValue();
        lightningDmg = _lightningDmg.getFinalValue();
        dmgAddon = _dmgAddon.getFinalValue();
        ModifierList = new List<float>();
    }

    public float getTotalDmg()
    {
        return getFinalValue()+fireDmg + iceDmg + lightningDmg+dmgAddon;
    }
        
}