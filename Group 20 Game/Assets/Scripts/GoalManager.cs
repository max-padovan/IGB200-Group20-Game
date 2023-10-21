using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    public InventoryManager iManager;
    public List<GoalDefinition> goals;
    private int currentGoal;

    public Text goalTitle;
    public Text goalDescription;
    public Text goalProgress;

    void Start()
    {
        //iManager = GameObject.Find("inventoryManager").GetComponent<InventoryManager>();

        currentGoal = 0;
        showGoalInfo(currentGoal); //show the right ingo from the get go
    }

    void Update()
    {
        //is there enough of this item to complete the goal?
        if (iManager.hasItems(goals[currentGoal].item, goals[currentGoal].amountToComplet))
        {
            if (currentGoal < goals.Count)
            {
                Reward(currentGoal);
                Debug.Log("Goal: " + currentGoal + " Completed!");
                currentGoal ++;
                showGoalInfo(currentGoal); //shows new goals info
            }
            else
            {
                Debug.Log("Youve completed all the goals");
                //Show text that says youve completed all the goals in book
            }
        }
        else //show current amount
        {
            if (iManager.howMuchItem(goals[currentGoal].item) == 999)
            {
                Debug.Log("ERROR: Item does not exist in players inventory, will return 999 as current amount in inventory under goal page in book");
            }
            else
            {
                goals[currentGoal].currentAmount = iManager.howMuchItem(goals[currentGoal].item);
                showGoalInfo(currentGoal); //just update the current goals progress
            }
        }
    }

    private void showGoalInfo(int currentGoal)
    {
        goalTitle.text = goals[currentGoal].goalName;
        goalDescription.text = goals[currentGoal].goalExplanation;
        goalProgress.text = goals[currentGoal].currentAmount.ToString() + " /" + goals[currentGoal].amountToComplet;
    }

    private void Reward(int currentGoal)
    {
        foreach(Item rewardItem in goals[currentGoal].rewardItems)
        {
            iManager.AddItem(rewardItem);
        }
    }
}