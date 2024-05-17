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
    private bool wasStopped;
    private float returningSpeed;

    private float maxDistanceAmongSword;
    // Start is called before the first frame update
    [Header("bounce info")] private bool isBouncing = false;
    private int amountOfBounce;
    public List<Transform> enemyTarget;
    public int targetIndex=1;
    [SerializeField] private float bounceSpeed;
    [Header("pierce info")]
    private int pierceAmount=0;

    [Header("spin info")] 
    private float spinTimer;
    private bool isSpin;
    private float hitColdown;
    private float hitTimer;
    private void Awake()
    {
        swordAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void Start()
    {
        
    }

     public void setMaxDistance(float max)
    {
        maxDistanceAmongSword = max;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        stuckInto(collider);
        collider.GetComponent<enemy>()?.damageEffect();
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

    public void setUpPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    public void setUpSpin(float spinDuration,float _hitColdown)
    {
        isSpin = true;
        spinTimer = spinDuration;
        hitColdown = _hitColdown;
    }
    private void stuckInto(Collider2D collider)
    {
        if (collider.GetComponent<enemy>()!=null &&pierceAmount>0)
        {
            pierceAmount--;
            return;
        }
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing)
        {
            return;
        }

        canRotate = false;
        transform.parent = collider.transform;
        wasStopped = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerManager.instance.player.transform.position,transform.position)>maxDistanceAmongSword)
        {
            playerManager.instance.player.deleteSword();
        }
        if (canRotate)
        {
            transform.right = rb.velocity;
        }

        if (returning)
        {   
            transform.position = Vector2.MoveTowards(transform.position,
                playerManager.instance.player.transform.position, returningSpeed);
            if (Vector2.Distance(transform.position,playerManager.instance.player.transform.position)<1)
            {   
            
                playerManager.instance.player.deleteSword();
                playerManager.instance.player.stateMachine.ChangeState(playerManager.instance.player.playerCatchSwordState);
                isBouncing = false; 
            }
        }

        bounceLogic();
        spinLogic();

    }
    
    private void bounceLogic()
    {
        if (isBouncing)
        {
            swordAnimator.SetBool("spin",true);
        }
        if (isBouncing && enemyTarget.Count >1&& amountOfBounce>0)
        {   
            
            transform.position =
                Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position)<0.1f)
            {   
                enemyTarget[targetIndex].GetComponent<enemy>().damageEffect();
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

    private void spinLogic()
    {
        if (wasStopped)
        {
            spinTimer -= Time.deltaTime;
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {   
                hitTimer = hitColdown;
                Collider2D[] cd = Physics2D.OverlapCircleAll(transform.position, 1);
                foreach (var hit in cd)
                {
                    if (hit.GetComponent<enemy>() != null)
                    {
                        hit.GetComponent<enemy>().damageEffect();
                    }
                }
            }
        }

        if (isSpin&&spinTimer<0)
        {
            isSpin = false;
            swordAnimator.SetBool("spin",false);
            callBackSword(bounceSpeed*Time.deltaTime);
        }
        if (isSpin )
        {
            swordAnimator.SetBool("spin",true);
        }
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

    }
}
