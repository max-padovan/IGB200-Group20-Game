using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(menuName ="Scriptable Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public ItemType type;
    public ActionType actionType;

    public bool stackable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public enum ItemType
    {
        seed,
        produce, 
        tool
    }
    public enum ActionType
    {
        dig,
        water,
        plant
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
