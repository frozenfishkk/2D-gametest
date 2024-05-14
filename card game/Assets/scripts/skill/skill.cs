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
    public Transform findClosestEnemy(Transform _transform)
    {
        Transform closestenemy = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 10);
        float minDistance = Mathf.Infinity;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                float distance = Vector2.Distance(_transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestenemy = hit.GetComponent<enemy>().transform;
                }
            
            }
            
        }

        return closestenemy;


    }

    protected virtual void Start()
    {
        player = playerManager.instance.player;
    }

    protected virtual void Update()
    {
        coldDownTimer-=Time.deltaTime;
    }
}
