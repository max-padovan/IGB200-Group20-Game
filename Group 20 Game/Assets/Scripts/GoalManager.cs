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

    public Notification notification;
    public bool currentTaskComplete = false;
    public AudioSource errorSound;
    public AudioSource goalCompleteSound;

    void Start()
    {
        //iManager = GameObject.Find("inventoryManager").GetComponent<InventoryManager>();

        currentGoal = 0;
        showGoalInfo(currentGoal); //show the right ingo from the get go
    }

    void Update()
    {

        int currentAmount = iManager.getItemCount(goals[currentGoal].item); //check how much the player currently has
        goals[currentGoal].currentAmount = currentAmount; //update that value for the goal
        showGoalInfo(currentGoal); //display the new value for the player

        if ( currentAmount >= 1 && !currentTaskComplete) //if there's any at all
        {
            if (currentAmount >= goals[currentGoal].amountToComplet) //if it's enough for the goal
            {
                currentTaskComplete = true; //can now go submit the goal for rewards
                notification.notif("You have the required produce for your task! Go to the journal to submit!");
            }
        }
        /*
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
                //Debug.Log("ERROR: Item does not exist in players inventory, will return 999 as current amount in inventory under goal page in book");
            }
            else
            {
                goals[currentGoal].currentAmount = iManager.howMuchItem(goals[currentGoal].item);
                showGoalInfo(currentGoal); //just update the current goals progress
            }
        }
        */
    }


    public void SubmitGoal()
    {
        if (currentGoal < goals.Count && currentTaskComplete) //they finished the current and there's another to do
        {
            goalCompleteSound.Play();
            Reward(currentGoal);
            notification.notif("Goal " + currentGoal + " Completed!");
            Debug.Log("Goal: " + currentGoal + " Completed!");
            currentTaskComplete = false;
            currentGoal++;
            showGoalInfo(currentGoal); //shows new goals info
        }
        else if(currentTaskComplete) //they finished the current but there's no more to do
        {
            goalCompleteSound.Play();
            notification.notif("Congratulations! You've completed all the goals!");
            Debug.Log("Youve completed all the goals");
            //Show text that says youve completed all the goals in book
        }
        else //they haven't finished the current
        {
            errorSound.Play();
            notification.notif("You haven't met the requirements yet! Keep farming.");
            Debug.Log("You haven't met the requirements yet!");
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