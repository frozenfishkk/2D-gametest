using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarUI : MonoBehaviour
{
    private Entity entity;
    private Slider slider;
    private RectTransform transform;
    private charaterStats stats;
    // Start is called before the first frame update
    private void flipUI()
    {   
        transform.Rotate(0,180,0);
    }

    private void changeHealthBar()
    {           
        slider.maxValue = stats.maxHP.getFinalValue();
        slider.value = stats.currentHP;
    }
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        entity = GetComponentInParent<Entity>();
        stats = entity.GetComponent<charaterStats>();
        entity.onFlipped += flipUI;
        transform = GetComponent<RectTransform>();
        stats.onHealthChanged += changeHealthBar;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnDisable()
    {
        entity.onFlipped -= flipUI;
        entity.charaterStats.onHealthChanged -= changeHealthBar;
    }
}
