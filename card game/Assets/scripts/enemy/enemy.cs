using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : Entity
{
    [Header("Attack info")]
    public float attackDistance;
    public float attackColdDown;
    public float attackTimer;
    public enemyStateMachine stateMachine { get; private set; }
    #region move
    public float moveSpeed;
    public float idleTime;

    #endregion
    [SerializeField] protected LayerMask player;
    public float playerDetectDistance;
    [Header("stun info")]
    public float stunTime;
    protected bool canStunned;
    [SerializeField] protected GameObject counterImage;
    protected override void Awake()
    {   
        base.Awake();
        stateMachine = new enemyStateMachine();
    }


    protected override void Start()
    {
        base.Start();
    }
    protected override void  Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D playerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerDetectDistance,player);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.transform.position, new Vector3(wallCheck.transform.position.x + attackDistance*facingDir, wallCheck.transform.position.y));
    }
    public virtual void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger(); 
    }

    public virtual void openCounterAttackWindow()
    {
        canStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void closeCounterAttackWindow()
    {
        canStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool canBeStunned()
    {
        if (canStunned)
        {
            return true;
        }
        return false;
    }
}
