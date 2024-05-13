using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class skillManager : MonoBehaviour
{
    public static skillManager instance;
     public dashSkill dashSkill { get; private set; }
     public clone_skill cloneSkill { get; private set; }
     public throwSwordSkill throwSkill { get; private set; }

     public blackHoleSkill blackHoleSkill{ get; private set; }
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        dashSkill = GetComponent<dashSkill>();
        cloneSkill = GetComponent<clone_skill>();
        throwSkill = GetComponent<throwSwordSkill>();
        blackHoleSkill = GetComponent<blackHoleSkill>();
    }
}
