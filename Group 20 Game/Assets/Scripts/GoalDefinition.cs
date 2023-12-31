using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName = "ScriptableObjects/GoalScriptableObject", order = 1)]
public class GoalDefinition : ScriptableObject
{
    public string goalName;
    public string goalExplanation;
    public Item item;
    public int amountToComplet;
    [HideInInspector] public int currentAmount = 0; //is current amount of item untill completion of goal

    public List<Item> rewardItems;
    public List<Item> rewardUpgrades;
}