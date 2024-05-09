using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{   
    [SerializeField] protected float coldDown;
    protected float coldDownTimer;
    protected player player;
    private void Awake()
    {
        
    }
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

    private void Start()
    {
        player = playerManager.instance.player;
    }

    protected virtual void Update()
    {
        coldDownTimer-=Time.deltaTime;
    }
}
