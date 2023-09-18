using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject book;
    private bool isBookOpen = false;

    void Start()
    {
        //book.gameObject.SetActive(false);
    }

    void Update()
    {
        Book();
    }

    private void Book() //Book Manager
    {
        
        if (Input.GetKeyDown(KeyCode.Tab) && isBookOpen == true)
        {
            book.gameObject.SetActive(true);
            isBookOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isBookOpen == false)
        {
            book.gameObject.SetActive(false);
            isBookOpen = true;
        }
    }
}
