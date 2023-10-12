using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Plugs
    public GameObject bookLower;
    public GameObject bookUpper;
    public bool bookActive = false;

    public GameObject shedUI;
    private bool shedActive = false;

    public GameObject waterUI;
    private bool waterActive = false;

    public GameObject hotbar;

    public GameObject doorUI;
    private bool doorActive = false;

    public GameObject goalUI;
    private bool goalActive = true;

    public camera cam;
    #endregion

    void Start()
    {
        bookLower.SetActive(false);
        bookUpper.SetActive(false);
        shedUI.SetActive(false);
        waterUI.SetActive(false);
        hotbar.SetActive(true);
        doorUI.SetActive(false);
        goalUI.SetActive(true);
    }

    void Update()
    {
        OpenBook();
    }

    #region Book
    public void OpenBook() //Book Manager
    {
        if (Input.GetKeyDown(KeyCode.Tab) && shedActive == false && waterActive == false && doorActive == false)
        {
            bookActive = true;

            bookLower.SetActive(true);
            bookLower.GetComponent<Book>().openLastOpenedPage();
            bookUpper.SetActive(true);
            shedUI.SetActive(false);
            waterUI.SetActive(false);
            doorUI.SetActive(false);
            hotbar.SetActive(false);
            goalUI.SetActive(false);
            
            cam.canPan = false;
        }
    }

    public void CloseBook() //Is used by the exit button in book
    {
        bookActive = false;
        
        bookLower.SetActive(false);
        bookLower.GetComponent<Book>().CloseBook();
        bookUpper.SetActive(false);


        cam.canPan = true;

        hotbar.SetActive(true);
        goalUI.SetActive(true);
    }
    #endregion
    #region Shed

    public void ClickOnShed()
    {
        if (Input.GetMouseButtonUp(0) && cam.canPan && bookActive == false && waterActive == false)
        {
            shedActive = true;
        
            shedUI.SetActive(true);
            cam.canPan = false; 

            goalUI.SetActive(false);
        }
    }
    
    public void ESCOffShed()
    {
        if (Input.GetKey("escape") && shedActive == true && bookActive == false && waterActive == false)
        {
            shedActive = false;
            
            shedUI.SetActive(false);
            cam.canPan = true;

            goalUI.SetActive(true);
        }
    }

    public void ClickOffShed()
    {
        shedActive = false;
        shedUI.SetActive(false);
        cam.canPan = true;

        goalUI.SetActive(true);
    }

    #endregion
    #region Water

    public void ClickOnWater()
    {
        if (Input.GetMouseButtonUp(0) && cam.canPan && bookActive == false && shedActive == false && doorActive == false)
        {
            waterActive = true;
            waterUI.SetActive(true);
            cam.canPan = false;

            goalUI.SetActive(false);
        }
    }

    public void ESCOffWater()
    {
        if (Input.GetKey("escape") && waterActive == true && bookActive == false && shedActive == false && doorActive == false)
        {
            waterActive = false;
            
            waterUI.SetActive(false);
            cam.canPan = true;

            goalUI.SetActive(true);
        }
    }

    public void ClickOffWater()
    {
        waterActive = false;
        waterUI.SetActive(false);
        cam.canPan = true;

        goalUI.SetActive(true);
    }

    #endregion
    #region Next Day

    public void ClickOnDay()
    {
        if (Input.GetMouseButtonUp(0) && cam.canPan && bookActive == false && shedActive == false && waterActive == false)
        {
            doorActive = true;
            doorUI.SetActive(true);
            cam.canPan = false;

            goalUI.SetActive(false);
        }
    }

    public void ESCOffDay()
    {
        if (Input.GetKey("escape") && doorActive == true && bookActive == false && shedActive == false && waterActive == false)
        {
            doorActive = false;
            
            doorUI.SetActive(false);
            cam.canPan = true;

            goalUI.SetActive(true);
        }
    }

    public void ClickOffDay()
    {
        doorActive = false;
        doorUI.SetActive(false);
        cam.canPan = true;

        goalUI.SetActive(true);
    }
    #endregion
}