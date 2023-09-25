using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant")]
public class plantInfo : ScriptableObject
{
    /// <summary>
    /// This script is just used to store all the information relevant to the specific plant type as a scriptable object
    /// </summary>




    public int plantHealth; //what this plant types max health is (in case we want some plants to be hardier)
    public int daysToGrow; //how many in-game days it will take to reach fruiting stage

    //could add different vars to have this for different stages, or just have it as a fraction of this (0/3 for seedling, 1/3 for young, 2/3 for mature, 3/3 for fruiting)
    //and round it ofc
    
    public type plantName; //just as an enum in case we spell it wrong in editor ig?
    public LightRequirement lightRequirement; // same deal, this just tells what type of planter it needs to be in
    public int waterRequirement; //its ideal water req/day
    public int maxFruit; // what the player actually gets will be a ratio of this * plant health

    public int minFruit; //just to make sure it doesn't go below this

    //we could have the amount of health lost for incorrect care change, or we can have it constant for all plants and just adjust its max health for balancing how hardy it is


    //public GameObject assetContainer;

    public GameObject[] PlantStates; //easier way of storing the assets for each state
    //seedling, young healthy, young unhealthy, mature healthy, mature unhealthy, fruiting, dead
    //^seedling and dead could be the same for every plant if we want to save peter some work



    public enum type
    {
        Test, 
        Tomato,
        Carrot
    }
    public enum LightRequirement
    {
        Full,
        Half,
        None
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
