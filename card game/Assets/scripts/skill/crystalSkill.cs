using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class crystal_skill : skill
{
    [SerializeField] private GameObject crystal_prefab;
    private GameObject crystal;
    [SerializeField] private float crystalDuration;
    private crystalController controller;

    [Header("multi stack Crystal")] 
    [SerializeField] private int maxStack;
    [SerializeField]private float stackColdown;
    #region stack info
    private float stackTimer;
    private int currentStack;
    [SerializeField]private bool isStack;

    #endregion
    public override bool canUseSkill()
    {
        return base.canUseSkill();
    }

    protected override void Start()
    {
        base.Start();
        
    }



    public override void useSkill()
    {   
        base.useSkill();
        if (isStack)
        {
            if (currentStack>0)
            {
                crystal = Instantiate(crystal_prefab, player.transform.position, quaternion.identity);
                controller = crystal.GetComponent<crystalController>();
                controller.setUpCrystal(crystalDuration);
                currentStack--;
            }
            return;
        }
        if (crystal == null)
        {
            crystal = Instantiate(crystal_prefab, player.transform.position, quaternion.identity);
            controller = crystal.GetComponent<crystalController>();
            controller.setUpCrystal(crystalDuration);
        }
        else if (crystal != null && controller.canSwitch)
        {
            Vector2 playerPos = player.transform.position;
            player.transform.position = crystal.transform.position;
            crystal.transform.position = playerPos;
            controller.crystalOverTime();
        }
    }

    protected override void Update()
    {
        base.Update();
        stackTimer -= Time.deltaTime;
        if (currentStack == maxStack)
        {
            stackTimer = stackColdown;
        }
        if (stackTimer<0 && currentStack<maxStack)
        {
            stackTimer = stackColdown;
            currentStack++;
        }
    }
}