using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class bgDim : MonoBehaviour
{
    public camera cam; //camera script reference
    public GameObject shedUI; //the shed UI so it can be toggled
    public Button BG; //button element on the bg dim

    void Start()
    {
        BG.onClick.AddListener(buttonPressed); //calls buttonbressed when clicked on
    }

    void buttonPressed()
    {
        shedUI.SetActive(false); //makes it invisible
        cam.canPan = true; //allows the player to move cam again
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
