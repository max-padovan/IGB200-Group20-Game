using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Plugs
    public GameObject book;
    private bool bookActive = false;

    public GameObject shedUI;
    private bool shedActive = false;

    public GameObject waterUI;
    private bool waterActive = false;

    public camera cam;
    #endregion

    void Start()
    {
        book.SetActive(false);
        shedUI.SetActive(false);
        waterUI.SetActive(false);
    }

    #region Book
    public void OpenBook() //Book Manager
    {
        if (Input.GetKeyDown(KeyCode.Tab) && shedActive == false && waterActive == false)
        {
            bookActive = true;

            book.SetActive(true);
            shedUI.SetActive(false);
            waterUI.SetActive(false);
            cam.canPan = false;
        }
    }

    public void CloseBook() //Is used by the exit button in book
    {
        bookActive = false;
        
        book.SetActive(false);
        cam.canPan = true;
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
        }
    }
    
    public void ClickOffShed()
    {
        if (Input.GetKey("escape") && shedActive == true && bookActive == false && waterActive == false)
        {
            shedActive = false;
            
            shedUI.SetActive(false);
            cam.canPan = true;       
        }
    }

    #endregion
}