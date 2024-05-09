using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwSwordController : MonoBehaviour
{   
    
    private Animator swordAnimator;
    private bool canRotate = true;
    private Rigidbody2D rb;
    private bool returning;
    private Collider2D cd;
    
    private float returningSpeed;
    // Start is called before the first frame update
    [Header("bounce info")] private bool isBouncing = false;
    private int amountOfBounce;
    public List<Transform> enemyTarget;
    public int targetIndex=1;
    [SerializeField] private float bounceSpeed;
    private void Awake()
    {
        swordAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        stuckInto(collider);
        if (collider.GetComponent <enemy>() !=null)
        {
            if (enemyTarget .Count<=0 &&isBouncing)
            {
                Collider2D[] cd = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in cd)
                {
                    if (hit.GetComponent<enemy>() !=null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }




        }
    }

    public void setUpBounce(bool _isbounce,int _amountOfBounce)
    {
        isBouncing = _isbounce;
        amountOfBounce = _amountOfBounce;
    }
    private void stuckInto(Collider2D collider)
    {
        
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing)
        {
            return;
        }

        canRotate = false;
        transform.parent = collider.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }

        if (returning)
        {   
            transform.position = Vector2.MoveTowards(transform.position,
                playerManager.instance.player.transform.position, returningSpeed);
        }

        bounceLogic();
    }

    private void bounceLogic()
    {
        if (isBouncing && enemyTarget.Count >1&& amountOfBounce>0)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position)<0.1f)
            {
                transform.parent = null;
                targetIndex++;
                amountOfBounce--;
                if (targetIndex >=enemyTarget.Count )
                {
                    targetIndex = 0;
                }

            }

            
        }

        if (amountOfBounce ==0 &&isBouncing)
        {
                
            callBackSword(bounceSpeed*Time.deltaTime);
        }
    }

    private void bounceSword()
    {
        
    }

    public void throwSword(Vector2 launchSpeed ,float swordGravity)
    {
        rb.velocity = launchSpeed;
        rb.gravityScale = swordGravity;
    }
        
    public void swordLanded()
    {
        swordAnimator.SetBool("spin",false);
        rb.velocity = new Vector2(0, 0);
    }

    public void callBackSword(float _returnningSpeed)
    {   
        Debug.Log("call back");
        rb.isKinematic = true;
        transform.parent = null;
        returning = true;
        returningSpeed = _returnningSpeed;
        if (Vector2.Distance(transform.position,playerManager.instance.player.transform.position)<1)
        {   
            
            playerManager.instance.player.deleteSword();
            playerManager.instance.player.stateMachine.ChangeState(playerManager.instance.player.playerCatchSwordState);
            isBouncing = false; 
        }
    }
}
