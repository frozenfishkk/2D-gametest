using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_skill : skill
{
    [SerializeField] private GameObject clone_prefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private float loseColorSpeed;
    
    public void createClone(Transform cloneTransform)
    {
        GameObject new_clone = Instantiate(clone_prefab);
        new_clone.GetComponent<cloneController>().setupClone(cloneTransform,cloneDuration,loseColorSpeed,playerManager.instance.player.animator.GetInteger("combocounter")+1);
        
    }
    public override bool canUseSkill()
    {
        return base.canUseSkill();
    }

    public override void useSkill()
    {
        base.useSkill();
    }


}
