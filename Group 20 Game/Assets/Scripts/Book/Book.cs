using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Book : MonoBehaviour
{
    public GameObject UIManager;
    public List<GameObject> Pages;
    private int lastOpenedPage = 0;
    private int currentPage;

    void Start()
    {
        //Load the last opened page
        Pages[lastOpenedPage].SetActive(true);
        currentPage = lastOpenedPage;
        Debug.Log(currentPage);
    }

    void Update()
    {
        
    }

    public void NextPage() //Attached to the next page button
    {
        if (currentPage < Pages.Count - 1)
        {
            Pages[currentPage].SetActive(false);
            currentPage ++;
            Debug.Log(currentPage);
            Pages[currentPage].SetActive(true);
            lastOpenedPage = currentPage;
        }
    }

    public void PreviousPage() //Attached to the previous page button
    {
        if (currentPage > 0)
        {
            Pages[currentPage].SetActive(false);
            currentPage --;
            Debug.Log(currentPage);
            Pages[currentPage].SetActive(true);
            lastOpenedPage = currentPage;
        }
    }

    public void CloseBook() //This is attached to the x in the book, there is another thats used in the UIManager. this one closes the pages
    {
        foreach (GameObject i in Pages)
        {
            i.SetActive(false);
        }
    }

    public void openLastOpenedPage() //is called in UIManager because of stupid setActive rules
    {
        Pages[lastOpenedPage].SetActive(true);
    }

    public void HomeTab() //Called by first tab
    {
        Pages[currentPage].SetActive(false);
        currentPage = 0; //Change this to change the page it opens
        Debug.Log(currentPage);
        Pages[currentPage].SetActive(true);
        lastOpenedPage = currentPage;

        //Add an icon change but will need to make it reset when:
        // - go to nect page
        // - or use another tab
        //Maybe put tab height logic in update
    }
}