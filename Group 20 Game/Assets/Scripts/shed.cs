using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shed : MonoBehaviour
{
    //MeshRenderer mesh;
    //Color original = Color.white;
    //Color hoverCol = Color.gray;
    public GameObject UIManager;

    void OnMouseOver()
    {
        UIManager.GetComponent<UIManager>().ClickOnShed();
    }

    void Update()
    {
        UIManager.GetComponent<UIManager>().ESCOffShed();
    }
}