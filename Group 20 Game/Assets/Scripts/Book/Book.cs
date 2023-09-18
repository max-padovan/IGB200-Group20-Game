using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject UIManager;
    
    public List<GameObject> Pages;

    void Update()
    {
        UIManager.GetComponent<UIManager>().OpenBook();
    }
}