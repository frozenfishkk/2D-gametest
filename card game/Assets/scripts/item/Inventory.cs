using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<itemData, item> InventoryDictionary;
    public Dictionary<itemData, item> stashDictionary;
    [SerializeField]public Dictionary<item_equip_data, item> equipmentDictionary;   
    public List<item> inventory;
    public List<item> stash;
    public List<item> equipment;
    [FormerlySerializedAs("itemSlotParent")] [SerializeField]private Transform inventorySlotParent; //parent;
    [SerializeField]private Transform stashParent;
    [SerializeField]private Transform equipmentParent;
    private slotContorller[] inventoryItemSlots;
    private slotContorller[] stashSlots;
    private equipSlotController[] equipSlots;
    [SerializeField]private List<itemData> startingEquipment;
    // Start is called before the first frame update
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
    void Start()
    {
        inventory  = new List<item>();
        InventoryDictionary = new Dictionary<itemData, item>();
        inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<slotContorller>();
        stashSlots = stashParent.GetComponentsInChildren<slotContorller>();
        equipSlots = equipmentParent.GetComponentsInChildren<equipSlotController>();
        stashDictionary = new Dictionary<itemData, item>();
        stash = new List<item>();
        equipment = new  List<item>();
        
        equipmentDictionary = new Dictionary<item_equip_data, item>();
        addStartingKit();
    }
    public void addStartingKit()
    {
        for (int i = 0; i < startingEquipment.Count; i++)
        {
            addItem(startingEquipment[i]);
        }
    }
    public void addItem(itemData itemData)
    {
        if (itemData.itemType==itemType.equipment)
        {
            addToInventory(itemData);
        }
        else if(itemData.itemType == itemType.material)
        {
            addToStash(itemData);
        }
        updateSlots();
    }

    public void addToInventory(itemData itemData)
    {
        if (InventoryDictionary.TryGetValue(itemData,out item value))
        {
            value.addStack();
        }
        else
        {   
            item newItem = new item(itemData);
            inventory.Add(newItem);
            InventoryDictionary.Add(itemData,newItem);
        }
    }

    public void addToStash(itemData itemData)
    {
        if (stashDictionary.TryGetValue(itemData,out item value))
        {
            value.addStack();
        }
        else
        {   
            item newItem = new item(itemData);
            stash.Add(newItem);
            stashDictionary.Add(itemData,newItem);
        }
    }

    private void removeFromStash(itemData itemData)
    {
        if (stashDictionary.TryGetValue(itemData,out item value))
        {
            value.removeStack();
            if (value.stackSize<=0)
            {
                stash.Remove(value);
                stashDictionary.Remove(itemData);
            }
        }
    }

    private void removeFromInventory(itemData itemData)
    {
        if (InventoryDictionary.TryGetValue(itemData,out item value))
        {
            value.removeStack();
            if (value.stackSize<=0)
            {
                inventory.Remove(value);
                InventoryDictionary.Remove(itemData);
            }
        }
    }
    public void removeItem(itemData itemData)
    {
        if (itemData.itemType == itemType.equipment)
        {
            removeFromInventory(itemData);
        }
        else if(itemData.itemType == itemType.material)
        {
            removeFromStash(itemData);
        }
        updateSlots();
    }

    public void clearInventorySlots()
    {
        for (int i = 0; i < inventoryItemSlots.Length; i++)
        {
            
            inventoryItemSlots[i].cleanSlot();
        }
    }
    public void equipItem(itemData itemData)
    {   
        
        item_equip_data equipData= itemData as item_equip_data;
        item newItem = new item(equipData);
        item oldItem = null;
        item_equip_data oldEquipData = null;
        foreach (var VARIABLE in equipmentDictionary)
        {
            if (VARIABLE.Key.equipmentType == equipData.equipmentType)
            {   
                oldItem = VARIABLE.Value;
                oldEquipData = VARIABLE.Key;
            }
        }
        if (oldItem!=null)
        {
            removeEquipFromEquipment(oldItem, oldEquipData);
        }
        equipment.Add(newItem);
        equipmentDictionary.Add(equipData,newItem);
        equipData.addModifier();
        removeFromInventory(itemData);
        clearInventorySlots();
        // updateSlots();
    }

    public void removeEquipFromEquipment(item oldItem, item_equip_data oldEquipData)
    {
        equipment.Remove(oldItem);
        equipmentDictionary.Remove(oldEquipData);
        oldEquipData.removeModifier();
        addToInventory(oldItem.Data);
    }

    public void updateSlots()
    {
        clearInventorySlots();
        for (int i = 0; i < equipSlots.Length; i++)
        {
            foreach (var VARIABLE in equipmentDictionary)
            {
                if (VARIABLE.Key.equipmentType == equipSlots[i].equipmentType)
                {
                    equipSlots[i].updateSlots(VARIABLE.Value);
                }
            }
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlots[i].updateSlots(inventory[i]);    
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashSlots[i].updateSlots(stash[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
