using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] item;
    public water waterManager;


    public void PickupItem(int id)
    {
        waterManager.UseWater(1);
        
        Debug.Log(item[id]);
        bool result = inventoryManager.AddItem(item[id]);
        if(result == true) 
        {
            Debug.Log("added");
        }
        else
        {
            Debug.Log("inv full");
        } 
        
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
