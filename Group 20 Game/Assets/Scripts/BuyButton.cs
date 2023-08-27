using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] item;


    public void PickupItem(int id)
    {
        Debug.Log(item[id]);
        inventoryManager.AddItem(item[id]);
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
