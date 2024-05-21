using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{   
    [SerializeField] private  itemData itemData;
    public Rigidbody2D rb;

    [SerializeField] private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = itemData.itemName;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    private void OnValidate()
    {
        
    }

    public void setUpItem(itemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        velocity = _velocity;
    }
    // Update is called once per frame

    void Update()
    {
        

    }


    public void pickUpItem()
    {
        Inventory.instance.addItem((itemData));
        
        Inventory.instance.updateSlots();
        Destroy(gameObject);
    }
}
