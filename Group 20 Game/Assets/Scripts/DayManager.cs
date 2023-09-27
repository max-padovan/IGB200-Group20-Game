using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public int Day = 0;
    public Button nextDayButton;
    public bool isRaining;
    public Animator dayAnim;
    public Image BlackFade; //don't think I need this?

    public float timer = 2f;
    public bool disableFade = false;
    public UIManager UI;
    public GameObject DayFade;
    public Notification notif;


    public void NextDay()//method for going to the next day
    {
        if(!disableFade)
        {
            Day += 1;
            DayFade.SetActive(true);
            Debug.Log("Next day!");
            disableFade = true;
            StartCoroutine(beginFade());
        }
        
    }



    IEnumerator beginFade() //animation stuff for next day
    {

        dayAnim.Play("DayFadeIn");
        //notificationTextUI.text = message;
        yield return new WaitForSeconds(timer);
        UI.ClickOffDay();
        StartCoroutine(endFade());
        notif.notif("It is now day " + Day); //Could move this if it looks weird
    }

    IEnumerator endFade() //more animation stuff
    {
        dayAnim.Play("DayFadeOut");
        disableFade = false;
        yield return new WaitForSeconds(timer);
        DayFade.SetActive(false);
        //Debug.Log(Day);
        
    }


    //If they press the button to go to the next day, add 1 to the day int, fade-in fade-out, do a weather check*


}
