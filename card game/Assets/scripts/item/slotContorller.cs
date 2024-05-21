using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class slotContorller : MonoBehaviour,IPointerDownHandler
{   
    [SerializeField] public Image itemImage;
    [SerializeField] public TextMeshProUGUI itemText;
    public item item;

    public virtual void Start()
    {
        
    }

    public virtual void updateSlots(item _item)
    {

        item = _item;
         itemImage.color = Color.white;
        itemImage.sprite = _item.Data.icon;
        itemText.text = _item.stackSize.ToString();

    }

    public  void cleanSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemText.text = "";
        itemImage.color = new Color(255,255,255,0.2f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.removeItem(item.Data);
            Inventory.instance.updateSlots();
            return;
        }
        if (item == null)
        {
            return;
        }
        if (item.Data.itemType ==itemType.equipment)
        {
            Inventory.instance.equipItem(item.Data);
            Inventory.instance.updateSlots();
        }
    }
}
