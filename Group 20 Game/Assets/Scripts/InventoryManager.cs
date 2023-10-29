using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryItemPrefab; 
    public invSlot[] inventorySlots;
    public Item[] items;
    //public invSlot[] hotbarSlots; maybe best not to distinguish
    public int stackSize = 10;
    public GameObject[] handItems;

    Item activeItem;
    int selectedSlot = 24;

    public void changeHandItem(Item item)
    {
        for(int i = 0; i < handItems.Length; i++)
        {
            if (items[i] == item)
            {
                handItems[i].SetActive(true);
            }
            else
            {
                handItems[i].SetActive(false);
            }
        }
    }
    void ChangeSlotSelected(int newSlot)
    {
        inventorySlots[selectedSlot].Deselect();
        if (newSlot >= 28)
        {
            selectedSlot = 24;
            inventorySlots[selectedSlot].Select();
            //selectedSlot = selectedSlot;
        }
        else if (newSlot <= 23)
        {
            selectedSlot = 27;
            inventorySlots[selectedSlot].Select();
            //  selectedSlot = selectedSlot;
        }
        else
        {
            inventorySlots[newSlot].Select();
            selectedSlot = newSlot;
        }

    }
    public bool AddItem(Item item) //number doesn't really work
    {
        //Debug.Log(item);
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
        //Debug.Log(item);
        inventoryItem.InitialiseItem(item);
    }
    public Item GetItemRef(int ID)
    {
        return items[ID];
    }
    public itemDrag QuerySelectedItemDrag()
    {
        invSlot slot = inventorySlots[selectedSlot];
        itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
        return itemInSlot;
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
                if (itemInSlot.count <= 0)
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
    void Start()
    {
        ChangeSlotSelected(24); //make acive slot the players first inv slot
        
        //Add 10 tomato seeds
        for (int i = 0; i < 10; i++)
        {
            AddItem(items[3]);
            AddItem(items[5]);
            AddItem(items[7]);
            AddItem(items[9]);
        }
    }
    void Update()
    {
        //this just keeps checking what's in the active slot
        activeItem = QuerySelectedItem(false);
        changeHandItem(activeItem);

        if(Input.mouseScrollDelta.y > 0)
        {
            //Debug.Log("Scrolled Up");
            ChangeSlotSelected(selectedSlot + 1);
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            //Debug.Log("Scrolled Down");
            ChangeSlotSelected(selectedSlot - 1);
        }

        //checking user input to change the selected hotbar slot
        /*
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int value);
            if (isNumber && value > 0 && value < 5) 
            {
                ChangeSlotSelected(value + 23); //+23 because of how the array of slots is stored, the hotbar ones are at the end
            }
        }
        */
    }



    public int getItemCount(Item item)
    {
        int count = 0; //using a count so it goes over every instance of an object, not just finding the first stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (itemInSlot != null && itemInSlot.item == item) //if the desired item is in the inventory
            {
                count += itemInSlot.count;
            }
        }
        return count;
    }

    public bool hasItems(Item item, int amount = 1) // Used in goal definition
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count >= amount) //if the desired item is in the inventory
            {
                return true;
            }
        }
        return false;
    }
    public int howMuchItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            invSlot slot = inventorySlots[i];
            itemDrag itemInSlot = slot.GetComponentInChildren<itemDrag>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                return itemInSlot.count;
            }
        }
        return 999;
    }
}