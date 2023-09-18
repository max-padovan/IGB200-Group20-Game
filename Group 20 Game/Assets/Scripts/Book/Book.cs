using System;
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
        Debug.Log("book is open");
    }
    void Update()
    {

    }

    private void showChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            //Debug.Log(transform.GetChild(i));
            //Debug.Log(transform.childCount);
        }
    }  
    private void hideChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            //Debug.Log(transform.GetChild(i));
        }
    }  
}        
            // shedUI.SetActive(false);
//             waterUI.SetActive(false);
//             cam.canPan = false;