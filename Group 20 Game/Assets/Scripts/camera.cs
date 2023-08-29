using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class camera : MonoBehaviour
{
    public bool isHome; //A way of determining where the camera should be for now
    private float panSpeed = 45; //how fast the panning is
    private float maxPanAngle = 0.5f; //how far left and right the player can pan to (~+-60 degrees when 0.5f)
    private float zoomScale = 1.5f; //how quickly the player zooms when scrolling...

    public bool canPan; //bool that checks if the player should be allowed to pan
    public bool runningCoroutine; //bool that is true when a coroutine is running
    public float mouseSensitivity = 3f; //changes the mouse pan sensitivity

    //Angle vars
    public Quaternion currentAngle;
    float y;
    float x;
    float z;

    //FOV vars
    float FOV;
    float maxFOV = 60;
    float minFOV = 25;

    //LERP vars
    float lerpSpeed = 0.05f;
    float lerpTime = 1f;
    float lerpProgress;
    public Vector3 homePos;
    public Vector3 camPos;
    public Vector3 vel = Vector3.zero;
    public Vector3 posi;
    public Quaternion inspectAngle;
    public Quaternion homeAngle;

    public GameObject planter;

    void Start()
    {
        //initialising some values
        currentAngle = this.transform.rotation;
        homeAngle = Quaternion.Euler(10, 0, 0);
        inspectAngle = Quaternion.Euler(35, 0, 0);
        homePos = transform.position;
        isHome = true; //Cam is in default position
        canPan = true; //
        runningCoroutine = false;

        z = 0;
        y = 0;
        x = 10;
        FOV = 60;
        posi = homePos;

        //planter = GameObject.Find()
    }

    void Update()
    {
        currentAngle = this.transform.rotation; //updates rotation data for the check method

        //Condition for camera positions
        if (isHome == true)
        {
            //normal cam functionality works
            if (canPan == true)
            {
                Pan();
                Zoom();
            }
        }
        else
        {
            //for returning to home pos
            if (Input.GetKeyDown(KeyCode.Escape) && !runningCoroutine)
            {
                Move(homePos, false);
                y = 0;
                isHome = true;               
            }
        }
    }

    private void Zoom() //camera zoom functionality
    {
        FOV -= Input.mouseScrollDelta.y * zoomScale;

        //conditions to limit zoom
        if (FOV > maxFOV) 
        { 
            FOV = maxFOV;
        }
        else if (FOV < minFOV) 
        {
            FOV = minFOV;
        }
        this.GetComponent<Camera>().fieldOfView = FOV;
    }

    private void Pan()
    {
        /*
        //Checks pan restrictions
        if (currentAngle.y < maxPanAngle) 
        {
            //can look right
            if (Input.GetKey("d"))
            {

                //scale the mouse 
                
                y += Time.deltaTime * panSpeed;
                this.transform.rotation = Quaternion.Euler(x, y, z); //using Euler made this so much easier holy
            }
        }

        if (currentAngle.y > -maxPanAngle)
        {
            //can look left
            if (Input.GetKey("a")) // a for now, can add mouse edge screen later
            {
                y -= Time.deltaTime * panSpeed;
                this.transform.rotation = Quaternion.Euler(x, y, z);
            }
        }
        */

        if (Input.GetMouseButton(0))
        {
            transform.eulerAngles += mouseSensitivity * new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0);
        }
    }
    
    public void Move(Vector3 pos, bool movingaway) //moves the camera to the vector3 pos, bool checks where it's going
    {
        if(!runningCoroutine)
        {
            runningCoroutine = true;
            if (movingaway)//moving away from home position
            {
                //isHome = false;
                this.GetComponent<Camera>().fieldOfView = maxFOV;//reset fov, little jarring **************
                posi = new Vector3(pos.x, pos.y + 7, pos.z - 7); //offset to see whole planter

                StartCoroutine(moveIn());

            }
            else //moving back to home position
            {
                StartCoroutine(moveBack());
            }
        }
    }

    //Lerp coroutine for smoothly moving cam to planter
    IEnumerator moveIn()
    {
        lerpProgress = 0;
        Vector3 currentPos = transform.position;
        Quaternion currentRot = transform.rotation;
        while (lerpProgress < lerpTime)
        {
            //was going to use SmoothDamp but it meant I had to redo some stuff. Might reconsider if the need arises
            //transform.position = Vector3.SmoothDamp(currentPos, posi, ref vel, 0.5f);
            transform.position = Vector3.Lerp(currentPos, posi, (lerpProgress / lerpTime));
            transform.rotation = Quaternion.Lerp(currentRot, inspectAngle, (lerpProgress / lerpTime));
            lerpProgress += Time.deltaTime;

            yield return null;
        }
        transform.position = posi;
        runningCoroutine = false;
        isHome = false;

        yield return null;
    }

    //Lerp coroutine for smoothly moving cam to home position
    IEnumerator moveBack()
    {
        lerpProgress = 0;
        Vector3 currentPos = transform.position;
        while (lerpProgress < lerpTime)
        {
            transform.position = Vector3.Lerp(currentPos, homePos, (lerpProgress / lerpTime));
            transform.rotation = Quaternion.Lerp(inspectAngle, homeAngle, (lerpProgress / lerpTime));
            lerpProgress += Time.deltaTime;

            yield return null;
        }
        transform.position = homePos;
        transform.rotation = homeAngle;
        currentAngle = homeAngle;
        runningCoroutine = false;

        yield return null;
    }
}