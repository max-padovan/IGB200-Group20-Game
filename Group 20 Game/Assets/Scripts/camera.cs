using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class camera : MonoBehaviour
{
    //GameObject cameraPrefab;
    public int pos; //A way of determining where the camera should be for now
    private float panSpeed = 45; //how fast the panning is
    private float maxPanAngle = 0.5f; //how far left and right the player can pan to (~+-60 degrees when 0.5f)
    private float zoomScale = 1.5f;
    Quaternion angle;
    float y;
    float x;
    float z;
    float FOV;


    // Start is called before the first frame update
    void Start()
    {
        pos = 0; //Cam is in default position
        angle = this.transform.rotation;
        z = 0;
        y = 0;
        x = 10;
        FOV = 60;
        
    }

    // Update is called once per frame
    void Update()
    {

        angle = this.transform.rotation;
        if (pos == 0)
        {
            
            Check();
            Zoom();
        }
        
    }

    private void Zoom()
    {
        FOV -= Input.mouseScrollDelta.y * zoomScale;
        if (FOV > 60) 
        { 
            FOV = 60;
        }
        else if (FOV < 25) 
        {
            FOV = 25;
        }
        this.GetComponent<Camera>().fieldOfView = FOV;
    }

    private void Check()
    {
        if (angle.y < maxPanAngle) 
        {
            //can look right
            if (Input.GetKey("d")) // d for now, can add mouse edge screen later
            {
                y += Time.deltaTime * panSpeed;
                this.transform.rotation = Quaternion.Euler(x, y, z); //using Euler made this so much easier holy
            }
        }

        if (angle.y > -maxPanAngle)
        {
            //can look left
            if (Input.GetKey("a")) // a for now, can add mouse edge screen later
            {
                y -= Time.deltaTime * panSpeed;
                this.transform.rotation = Quaternion.Euler(x, y, z);
            }
        }
    }

}
