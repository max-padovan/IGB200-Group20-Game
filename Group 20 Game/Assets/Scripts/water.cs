using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public Item item;
    //water tank
    public int maxWaterStorage = 100;
    public int currentWaterStorage = 100;
    public int dailyGain = 50;

    public InventoryManager inventoryManager;
    public itemDrag wateringCan; //Not sure if I'll need to get it or just make changes to its itemDrag

    public void refill() //Refill the amount in your watering can from the water tank
    {
        //watering can needs some missing + you must be holding it to refill
        Item activeItem = inventoryManager.QuerySelectedItem(false);
        if (activeItem != null && waterCount < maxWaterCan && activeItem.actionType == Item.ActionType.water)
        {
            Debug.Log("Refilling watering can!");
            int amountToRefill = maxWaterCan - waterCount; //how much it needs to be refilled by

            if (currentWaterStorage < amountToRefill)//if the amount in the tank isn't enough to fill completely
            {
                waterCount += currentWaterStorage;
                Debug.Log("You have run out of water in the tank!");
                currentWaterStorage = 0;
                tank.setWaterStorage(currentWaterStorage);
            }
            else
            {
                waterCount = maxWaterCan;
                currentWaterStorage -= amountToRefill;
                tank.setWaterStorage(currentWaterStorage);
            }
            //waterCount = maxWaterCan;
        }
        else if (waterCount == maxWaterCan)
        {
            Debug.Log("Your watering can is already full");
        }
        else
        {
            Debug.Log("Please hold your watering Can!");
        }
    }

    public void UpdateDailyGain(int newGain) //if the player upgrades how much they get per day
    {
        dailyGain = newGain;
        tank.dailyGain.text = "Your daily gain is: " + dailyGain;
    }
    public void AddDailyGain() // when the player goes to the next day and their tank refills
    {
        int missingWater = maxWaterStorage - currentWaterStorage;

        if(missingWater > dailyGain)
        {
            currentWaterStorage += dailyGain;
        }
        else
        {
            currentWaterStorage += missingWater;
        }
        tank.setWaterStorage(currentWaterStorage);
    }
    public void UseWater(int amount)
    {
        if(waterCount < amount)
        {
            Debug.Log("Sorry, you don't have that much water");
        }
        else
        {
            waterCount -= amount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        findWateringCan();
    }

    void findWateringCan() //kinda janky but it does the trick
    {
        wateringCan.count = waterCount;
        wateringCan.RefreshCount();
    }
    // Update is called once per frame
    void Update()
    {
        findWateringCan();
        //refillButton.onClick.AddListener(refill);
    }
}
