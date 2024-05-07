using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    [SerializeField] protected float coldDown;
    protected float coldDownTimer;

    public virtual bool canUseSkill()
    {
        if (coldDownTimer < 0)
        {
            coldDownTimer = coldDown;
            return true; 
        }

        return false;
    }

    public virtual void useSkill()
    {
        //use
        
        
    }
    private void Awake()
    {
        
    }

    protected virtual void Update()
    {
        coldDownTimer-=Time.deltaTime;
    }
}
