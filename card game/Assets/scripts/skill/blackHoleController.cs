using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class blackHoleController : MonoBehaviour
{
    private float blackHoleDuration;
    private float maxSize;
    private float growSpeed;
    private float blackHoleTimer;
    public List<Transform> enemyList; 
    private bool canGrow=true;
    private bool over=false;
    private float distanceBeside;
    private float cloneAttackTimer;
    [SerializeField]private List<GameObject> createdHotKey;
    private bool canCreateKey = true;
    [SerializeField] private int attackAmount;
    [SerializeField] private float cloneAttackColdown;
    [SerializeField] private GameObject playerClonePrefab;
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodes;
    [SerializeField] private float distanceAbove;
    // Start is called before the first frame update
    private void Start()
    {
        blackHoleTimer = blackHoleDuration;
    }

    public void setUpBlackHole(bool _canGrow,float _maxSize,float _growSpeed,float _distanceBeside,float _blackHoleDuration)
    {   
        canGrow = _canGrow;
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        distanceBeside = _distanceBeside;
        blackHoleDuration = _blackHoleDuration;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<enemy>()!=null &&canCreateKey)
        {
            collider.GetComponent<enemy>().freezeTime(true);
            GameObject hotkey = Instantiate(hotKeyPrefab,
                collider.transform.position + new Vector3(0, distanceAbove), quaternion.identity);
            KeyCode chosenKey = keyCodes[UnityEngine.Random.Range(0, keyCodes.Count)];
            hotkey.GetComponentInChildren<blackKeyHotkeyController>().setUpHotKey(chosenKey,collider.GetComponent<enemy>().transform,this);
            createdHotKey.Add(hotkey);
            keyCodes.Remove(chosenKey);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<enemy>()!=null )
        {
            collider.GetComponent<enemy>().freezeTime(false);


        }
    }

    public void setUpTarget(Transform trans)
    {
        if (enemyList.Contains(trans))
        {
            return;
        }
        enemyList.Add(trans);
    }

    public void createClone(List<Transform> _enemyList)
    {
        foreach (var enemy in _enemyList)
        {
            skillManager.instance.cloneSkill.createClone(enemy);
            attackAmount--;
        }
        
    }

    private void DestroyHotKey()
    {
        for (int i = 0; i < createdHotKey.Count; i++)
        {
                
                Destroy(createdHotKey[i]);
            
        }

    }
    
    // Update is called once per frame
    void Update()
    {   
        blackHoleTimer -= Time.deltaTime;

        if (canGrow && !over)
        {   
            Debug.Log("grow");
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize),
                growSpeed * Time.deltaTime);
        }

        if (blackHoleTimer<0 ||attackAmount<=0)
        {
            over = true;
        }
        if (over)
        {
            canCreateKey = false;
            DestroyHotKey();
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1),
                growSpeed * Time.deltaTime);

        }
        cloneAttackTimer -= Time.deltaTime;
        if (transform.localScale.x<0)
        {
            Destroy(gameObject);
            playerManager.instance.player.exitBlackHoleSkill();
            playerManager.instance.player.invis(false);
            skillManager.instance.blackHoleSkill.canUse = true;
            // playerManager.instance.player.invis(false);
        }
        if (cloneAttackTimer<0 &&attackAmount>0)
        {   
            playerManager.instance.player.invis(true);
            cloneAttackTimer = cloneAttackColdown;
            // blackHoleTimer = 20;
            createClone(enemyList);
            

        }
    }
}
