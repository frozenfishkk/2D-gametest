using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class blackHoleSkill : skill
{
    [SerializeField] private float blackHoleDuration;
    [SerializeField] private GameObject blackHole;
    [SerializeField] private bool canGrow;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float distanceBeside;
    public bool canUse= true;
    
    protected override void Update()
    {
        base.Update();
    }

    public override bool canUseSkill()
    {
        return base.canUseSkill();
    }

    public override void useSkill()
    {
        base.useSkill();
        
        if (canUse)
        {
            canUse = false;
            GameObject bh = Instantiate(blackHole,player.transform.position,quaternion.identity);
            bh.GetComponent<blackHoleController>().setUpBlackHole(canGrow,maxSize,growSpeed,distanceBeside,blackHoleDuration);
        }
        
    }

    protected override void Start()
    {
        base.Start();
        
        
    }
}
