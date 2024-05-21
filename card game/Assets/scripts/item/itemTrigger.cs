using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTrigger : MonoBehaviour
{
    private itemController item;

    

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponentInParent<itemController>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.GetComponent<player>()!=null)
        {
            item.pickUpItem();
        }
        
        
    }

}
