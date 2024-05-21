using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class itemDrop : MonoBehaviour
{
    [SerializeField]private GameObject itemPrefab;
    [SerializeField] private itemData itemData;
    [SerializeField] public List<itemData> possibleDrop;
    private List<itemData> dropList = new List<itemData>();

    [SerializeField] private int dropAmount;
    public void setUpDrop()
    {
        for (int i = 0; i < possibleDrop.Count; i++)
        {
            if (Random.Range(0,100)<possibleDrop[i].dropChance)
            {
                dropList.Add((possibleDrop[i]));
            }
        }
        
    }
    public void dropMultiItem()
    {   
        setUpDrop();
        for (int i = 0; i < dropList.Count; i++)
        {
            if (i<dropAmount)
            {
                itemData randomDrop = dropList[Random.Range(0, dropList.Count - 1)];
                dropItem(randomDrop);
                dropList.Remove(randomDrop);
            }
            
        }
    }
    public void dropItem(itemData _itemData)
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5f, 5f), 15f);
        item.GetComponent<itemController>().setUpItem(_itemData, randomVelocity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
