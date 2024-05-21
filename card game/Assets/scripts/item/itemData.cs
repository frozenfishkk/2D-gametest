using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType
{
    material,
    equipment,
    
}
[CreateAssetMenu(fileName = "itemData", menuName = "Data/Item")]
[Serializable]
public class itemData : ScriptableObject
{
    public itemType itemType;
    public string itemName;
    public Sprite icon;
    [Range(0,100)]
    public float dropChance;
}
