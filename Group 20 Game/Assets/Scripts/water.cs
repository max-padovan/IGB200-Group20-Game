using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water : MonoBehaviour
{
    //water storage vars
    public watertank tank;
    //public Item wateringCan;
    public Button refillButton;

    //watering can
    public int maxWaterCan = 25;
    public int waterCount = 0;
    //water tank
    public int maxWaterStorage = 100;
    public int currentWaterStorage = 100;

    public InventoryManager inventoryManager;
    public itemDrag wateringCan; //Not sure if I'll need to get it or just make changes to its itemDrag

    public void refill() //watering can needs some missing + you must be holding it to refill
    {
        Item activeItem = inventoryManager.QuerySelectedItem(false);

        if (waterCount < maxWaterCan && activeItem.actionType == Item.ActionType.water)
        {
            
            Debug.Log("REFILL???");
            int amountToRefill = maxWaterCan - waterCount; //how much it needs to be refilled by

            if(currentWaterStorage <  amountToRefill)//if the amount in the tank isn't enough to fill completely
            {
                waterCount += currentWaterStorage;
                currentWaterStorage = 0;
                tank.setWaterStorage(currentWaterStorage);
            }
            else
            {
                waterCount = maxWaterCan;
                currentWaterStorage -= amountToRefill;
                tank.setWaterStorage(currentWaterStorage);
            }
            waterCount = maxWaterCan;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
         

    }

    // Update is called once per frame
    void Update()
    {
        //refillButton.onClick.AddListener(refill);
    }



}
