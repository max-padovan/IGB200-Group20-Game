using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlant : MonoBehaviour
{

    Color original = Color.green;
    Color dry = Color.grey;
    
    private Vector3 startPos = new Vector3(0, 7f, 0);
    private Vector3 saplingPos = new Vector3(0, 1f, 0);
    private Vector3 adultPos = new Vector3(0, 18f, 0);

    private bool dead;
    private bool hydrated;

    private int newDay;
    
    private void Start()
    {
        dead = false;
        hydrated = false;
        gameObject.GetComponentInChildren<Transform>().position -= startPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.GetComponentInChildren<Transform>().position += saplingPos;
            
        }
    }

    private void death()
    {
        Destroy(gameObject);
    }

    public void waterCrop()
    {
        hydrated = true;
    }
}
