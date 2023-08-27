using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryItemPrefab; 
    public invSlot[] inventorySlots;
    public void AddItem(Item item)
    {
        Debug.Log(item);
        //look for empty slot in shed
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            //itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (slot.GetComponentInChildren<itemDrag>() == null)
            {
                SpawnItem(item,slot);
                return;
            }
        }
    }
    public void SpawnItem(Item item, invSlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        itemDrag inventoryItem = newItemGameObject.GetComponent<itemDrag>();
        Debug.Log(item);
        inventoryItem.InitialiseItem(item);

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
