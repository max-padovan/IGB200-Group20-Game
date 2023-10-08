using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Text notificationTextUI;
    [TextArea] public string message;
    public GameObject notifUI;
    private bool notifHappening = false;
    public bool disable = true;

    public float timer = 1.0f;

    public Animator notifAnim;
    

    //not box collider... maybe button?

    public void notif(string text)
    {
        if(!notifHappening)
        {
            notifHappening = true;
            //Debug.Log(1);
            message = text;
            //notifUI.SetActive(true);
            StartCoroutine(enableNotif());

        }

    }

    IEnumerator enableNotif()
    {
        
        notifAnim.Play("NotifAnimIN");
        notificationTextUI.text = message;
        if (disable)
        {
            yield return new WaitForSeconds(timer);
            RemoveNotif();
        }
    }

    void RemoveNotif()
    {
        //Debug.Log(2);
        notifAnim.Play("NotifAnimOUT");
        //wait??
        notifHappening = false;
        //notifUI.SetActive(false);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        //test
        notif("Tomatoes require 4 water a day for optimal growth!");
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
