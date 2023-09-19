using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject UIManager;

    void OnMouseOver()
    {
        UIManager.GetComponent<UIManager>().ClickOnDay();
    }

    void Update()
    {
        UIManager.GetComponent<UIManager>().ESCOffDay();
    }
}
