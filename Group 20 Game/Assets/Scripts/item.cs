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

    public plantInfo plant;
    public bool stackable = true;
    public bool waterCapacity = false;
    // Start is called before the first frame update

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
}
