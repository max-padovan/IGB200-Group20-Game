using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Item")]
public class plant : ScriptableObject
{
    
    public int plantHealth;
    public int daysToGrow;
    public string plantName;
    public LightRequirement lightRequirement;
    public int waterRequirement;
    public int maxFruit; // what the player actually gets will be a ratio of this * plant health

    //idk if you want gameobjects or sprites or if there's a better way of doing this..
    //maybe an enum of age? then change it elsewhere based on that
    public GameObject plantYoung;
    public GameObject plantGrowing;
    public GameObject plantFull;
    public GameObject plantFruiting;




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
