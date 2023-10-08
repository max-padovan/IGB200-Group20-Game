using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plant : MonoBehaviour
{
    public plantInfo plantinfo; //which plant this actually is
    public Transform location; //where it is (basically a grid coord), or can I just use this.transform since it'll be in the right spot
    public bool isPlanterRight; //perhaps just a way of checking if the planter it's in is the right condition
    public int amountWateredToday; //how much water it has been given today
    public int health; //the plants current health
    // Start is called before the first frame update

    
    public bool rightPlanter;

    public DayManager dayManager;
    public int currentDay;
    public int plantAge = 0;
    GameObject CurrentObject;

    int healthyLim;
    int deadLim = 0;
    int youngAgeLim;
    int matureAgeLim;
    int fruitingAgeLim;

    bool ishealthy = true;
    public bool alive = true;

    void Start()
    {
        healthyLim = (int)Mathf.Floor(plantinfo.plantHealth/2); // so if it gets to half max, it's considered unhealthy
        youngAgeLim = (int)Mathf.Floor(plantinfo.daysToGrow / 3); // if it gets to 1/3 of its grow time, it goes to young state
        matureAgeLim = (int)Mathf.Floor(2 * plantinfo.daysToGrow / 3); // if it's 2/3 of grow time, goes to mature
        fruitingAgeLim = plantinfo.daysToGrow; //if it reaches the full grow time, it's fruiting

        dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();
        health = plantinfo.plantHealth; //set the health to the max for this plant type
        amountWateredToday = 0;
        currentDay = dayManager.Day; // (getting the current day)
        checkPlanter();
        //Vector3 hm = new Vector3 (this.transform.position.x, this.transform.position.y+1, this.transform.position.z);
        CurrentObject = Instantiate(plantinfo.PlantStates[0], this.transform); //instantiate the seedling object
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
                AdjustHealth(-40); // Take some health ~~~~~~~~~~~~~~~~~~~~~~~~~ not sure how much
            }
            RefreshObject();
            amountWateredToday = 0; //reset back to 0
        }
        
    }


    void RefreshObject() //what we do is check if it meets any of the requirements to change appearance
    {
        //***I think it's going to delete and re-instantiate every plant at every day, could add a check within each if statement 
        //checking to see if currentObject is already that type, and if it isn't we do the change, if it is, we just ignore it

        ///0 = seedling
        ///1 = young + healthy
        ///2 = young + unhealthy
        ///3 = mature + healthy
        ///4 = mature + unhealthy
        ///5 = fruiting
        ///6 = dead
        if(!alive)
        {
            Destroy(CurrentObject);
            CurrentObject = Instantiate(plantinfo.PlantStates[6], this.transform, false);
        }
        else if (plantAge >= fruitingAgeLim)
        {
            Destroy(CurrentObject);
            CurrentObject = Instantiate(plantinfo.PlantStates[5], this.transform, false);
        }
        else if(plantAge >= matureAgeLim)
        {
            if (!ishealthy)
            {
                Destroy(CurrentObject);
                CurrentObject = Instantiate(plantinfo.PlantStates[4], this.transform, false);
            }
            else if (ishealthy)
            {
                Destroy(CurrentObject);
                CurrentObject = Instantiate(plantinfo.PlantStates[3], this.transform, false);
            }
        }
        else if(plantAge >= youngAgeLim)
        {
            if (!ishealthy)
            {
                Destroy(CurrentObject);
                CurrentObject = Instantiate(plantinfo.PlantStates[2], this.transform, false);
            }
            else if (ishealthy)
            {
                Destroy(CurrentObject);
                CurrentObject = Instantiate(plantinfo.PlantStates[1], this.transform, false);
            }
        }
        else
        {
            //CurrentObject;
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
        else if(health < 0)
        {
            //change state stuff (dead)
            alive = false;
            //healthStatus = "dead";
        }
        else if (health < healthyLim)
        {
            //change state stuff (unhealthy)
            ishealthy = false;
        }
        else
        {
            //plant is healthy still
            ishealthy = true;

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
            if (amountWateredToday == plantinfo.waterRequirement)
            {
                //overwatered. Do we punish excessive overwatering?
                //perhaps don't take much/any if it was only 1
                AdjustHealth(20);
            }
            else if (amountWateredToday == 0)
            {
                //didn't water at all, big punishment?
                AdjustHealth(-50);
            }
            else
            {
                //over or under watered by some amount
                AdjustHealth(-(Mathf.Abs(amountWateredToday - plantinfo.waterRequirement) * 10)); //-10* amount over/underwatered
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
