using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalController : MonoBehaviour
{
    private Animator animator;
    private bool canExists=true;
    private float crystalTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField]private bool canExplode;
    [SerializeField] private float growSpeed;
    private CircleCollider2D cd;
    [SerializeField]private float maxsize;
    private Transform closestEnemy;
    private bool canGrow;
    public bool canSwitch;
    [SerializeField] private float flySpeed;

    [SerializeField] private bool canFly;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
        closestEnemy = skillManager.instance.crystalSkill.findClosestEnemy(transform);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawSphere(gameObject.transform.position,explodeRadius);
    // }

    // Update is called once per frame
    private void AnimationExplodeTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, cd.radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                hit.GetComponent<enemy>().damageEffect();
            }
        }
    }

    private void moveTowards(Transform trans)
    {
        transform.position = Vector2.MoveTowards(transform.position, trans.position, flySpeed*Time.deltaTime);
    }

    private void AnimationExplodeEndTrigger()
    {
        Destroy(gameObject);
    }
    public void setUpCrystal(float _crystalDuration)
    {
        crystalTimer = _crystalDuration;
    }
    void Update()
    {
        flyToEnemy();
        if (Vector2.Distance(transform.position,closestEnemy.position)<1)
        {
            crystalOverTime();
        }
        crystalTimer -= Time.deltaTime;
        if (crystalTimer<0)
        {
            canExists = false;
        }
        
        growUp();
        if (!canExists)
        {
            crystalOverTime();
        }
    }

    private void flyToEnemy()
    {
        if (canFly )
        {
            canSwitch = false;
            moveTowards(closestEnemy);
        }
    }

    private void growUp()
    {
        if (canGrow)
        {
            gameObject.transform.localScale = Vector2.Lerp(gameObject.transform.localScale,
                new Vector2(maxsize, maxsize), growSpeed * Time.deltaTime);
            cd.radius = maxsize;
        }
    }

    public void crystalOverTime()
    {
        if (canExplode)
        {
            animator.SetBool("explode",true);
            canGrow = true;
            canFly = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
