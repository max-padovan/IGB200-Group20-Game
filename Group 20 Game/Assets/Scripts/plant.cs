using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public plantInfo plantinfo; //which plant this actually is
    public Transform location; //where it is (basically a grid coord), or can I just use this.transform since it'll be in the right spot
    public bool isPlanterRight; //perhaps just a way of checking if the planter it's in is the right condition
    public int amountWateredToday; //how much water it has been given today
    public int health; //the plants current health
    // Start is called before the first frame update

    public bool alive = true;
    public bool rightPlanter;

    public DayManager dayManager;
    public int currentDay;
    public int plantAge = 0;

    void Start()
    {
        dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();
        health = plantinfo.plantHealth; //set the health to the max for this plant type
        amountWateredToday = 0;
        currentDay = dayManager.Day; // (getting the current day)
        checkPlanter();
    }

    // Update is called once per frame
    void Update()
    {
        if(dayManager.Day > currentDay) //ie the player went to the next day
        {
            currentDay++;//so it shouldn't be greater until they go next day again
            plantAge++; 
            checkWaterReq(); //check the water, adjust health as necessary
            if(!rightPlanter) //basically just take health if it's not right
            {
                //AdjustHealth(-20); // Take some health ~~~~~~~~~~~~~~~~~~~~~~~~~ not sure how much
            }
        }
    }

    void AdjustHealth(int amount) //adjust the health, if it's taking away, make the unput NEGATIVE
    {
        //int missingHealth = plantinfo.plantHealth - health; 
        health += amount;
        if(health > plantinfo.plantHealth) //if it went over the max, just set it back to max
        {
            health = plantinfo.plantHealth;
        }
        else if(health < (plantinfo.plantHealth*0.66))
        {
            //change state stuff (unhealthy)
        }
        else if (health < (plantinfo.plantHealth * 0.33))
        {
            //change state stuff (very unhealthy~~ we might not add this state though)
        }
        else if(health < 0)
        {
            //the plant has died...
            //set state to dead
            alive = false;
                //what does this actually do? Stop them from watering it? do they need to use a tool to clear it?
        }


    }
    void checkWaterReq()
    {
        //First, check if it's raining
        if(dayManager.isRaining)
        {
            //we don't need to bother checking
        }
        else
        {
            //we check how much they watered it
            //int absDifference = amountWateredToday - plantinfo.waterRequirement; //take abs of this, think I need math thing

            // call at the end of the day, and check how much the plant has been watered vs its required amount and adjust the health based on that
            if (amountWateredToday > plantinfo.waterRequirement)
            {
                //overwatered. Do we punish excessive overwatering?
                //perhaps don't take much/any if it was only 1
            }
            else if (amountWateredToday == plantinfo.waterRequirement)
            {
                //watered the perfect amount, give health
            }
            else if (amountWateredToday == 0)
            {
                //didn't water at all, big punishment?
            }
        }



    }

    void gettingWatered(int amount) //to call when using the watering can?
    {
        amountWateredToday += amount;
    }
    void checkPlanter()
    {
        //I need a way to check which planter type the node is in? or just the planter type itself
        //maybe parent parent parent tag, and have each planter have the tag of what type it is
        //if(tag == plantinfo.LightRequirement)
        //{rightPlanter = true;}
        // if not, make it false
    }
}
