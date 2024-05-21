using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
public class equipSlotController : slotContorller
{
    public equipmentType equipmentType;
    public override void Start()
    {
        base.Start();
        
    }

    
    private void OnValidate()
    {
        gameObject.name = equipmentType.ToString();
    }

    public override void updateSlots(item _item)
    {
        base.updateSlots(_item);
        itemText.text = "";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {   
        if (item == null)
        {
            return;
        }
        item_equip_data equipData = item.Data as item_equip_data;
        Inventory.instance.removeEquipFromEquipment(item, equipData);
        cleanSlot();
        Inventory.instance.updateSlots();
        
    }
}
