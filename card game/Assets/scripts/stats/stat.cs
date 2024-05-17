using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class stat
{
    [SerializeField]protected List<float> ModifierList;
    [SerializeField]private float baseValue;
    [SerializeField]private float finalValue;
    public float getBaseValue()
    {
        return baseValue;
    }

    public void setDefaultValue(float value)
    {
        baseValue = value;
    }
    public float getFinalValue()
    {
        finalValue = baseValue;
        foreach (var modify in ModifierList)
        {
            finalValue += modify;
        }
        
        return finalValue;
    }

    public void addModifer(float modify)
    {
        ModifierList.Add(modify);
    }
    public void removeModifer(float modify)
    {
        ModifierList.Remove(modify);
    }
}
