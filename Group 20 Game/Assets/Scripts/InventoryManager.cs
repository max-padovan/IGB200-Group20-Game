using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryItemPrefab; 
    public invSlot[] inventorySlots;
    public Item[] items;
    //public invSlot[] hotbarSlots; maybe best not to distinguish
    public int stackSize = 10;

    int selectedSlot = -1;



    void ChangeSlotSelected(int newSlot)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newSlot].Select();
        selectedSlot = newSlot;
    }

    public bool AddItem(Item item)
    {
        Debug.Log(item);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (itemInSlot!= null && itemInSlot.item == item && itemInSlot.item.stackable && itemInSlot.count < stackSize )
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //look for empty slot in shed
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            //itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (slot.GetComponentInChildren<itemDrag>() == null)
            {
                SpawnItem(item,slot);
                return true;
            }
        }
        return false;
    }
    public void SpawnItem(Item item, invSlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        itemDrag inventoryItem = newItemGameObject.GetComponent<itemDrag>();
        Debug.Log(item);
        inventoryItem.InitialiseItem(item);
    }
    public Item GetItemRef(int ID)
    {
        return items[ID];
    }
    public Item QuerySelectedItem(bool use)
    {
        invSlot slot = inventorySlots[selectedSlot];
        itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
        if(itemInSlot!= null)
        {
            Item item =  itemInSlot.item;
            if(use)
            {
                itemInSlot.count--;
                if(itemInSlot.count >= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        else
        {
            return null;
        }
    }
    


    // Start is called before the first frame update
    void Start()
    {
        ChangeSlotSelected(24);
        AddItem(items[1]);
    }

    // Update is called once per frame
    void Update()
    {

        //checking user input to change the selected hotbar slot
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int value);
            if (isNumber && value > 0 && value < 5) 
            {
                ChangeSlotSelected(value + 23); //+23 because of how the array of slots is stored, the hotbar ones are at the end
            }
        }
    }
}
