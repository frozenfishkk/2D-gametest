using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class blackKeyHotkeyController : MonoBehaviour
{
    private KeyCode hotKey;
    private Collider2D collision;
    private TextMeshProUGUI textMeshPro;
    private Transform enemyTransform;
    public List<Transform> enemyList;
    private SpriteRenderer sr;
    public blackHoleController blackHole;
    // Start is called before the first frame update
    public void setUpHotKey(KeyCode key,Transform enemyTrans,blackHoleController _blackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        hotKey = key;
        textMeshPro.text = hotKey.ToString();
        enemyTransform = enemyTrans;
        blackHole = _blackHole;
    }
    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hotKey))
        {
            sr.color = Color.clear;
            blackHole.setUpTarget(enemyTransform);
            textMeshPro.color = Color.clear;
        }
    }
}
