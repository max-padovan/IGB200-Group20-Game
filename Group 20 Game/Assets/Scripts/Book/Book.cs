using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public List<GameObject> Pages;
    public GameObject shedUI;
    public GameObject waterUI;
    public camera cam;

    private bool isMenuOpen = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && isMenuOpen == false) //open menu
        {
            isMenuOpen = !isMenuOpen; //set it to true

            //Do menu things ----------------
            shedUI.SetActive(false);
            waterUI.SetActive(false);
            cam.canPan = false;
            gameObject.SetActive(true);
            Debug.Log("it works"); 
        }
        
        if (Input.GetKey(KeyCode.Tab) && isMenuOpen == true) //close the menu
        {
            isMenuOpen = !isMenuOpen; //set it to false
            gameObject.SetActive(false);

            cam.canPan = true;
        }
    }
}