using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shed : MonoBehaviour
{
    MeshRenderer mesh;
    Color original = Color.white;
    Color hoverCol = Color.gray;
    public GameObject shedUI;
    public camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //shedUI = GameObject.FindGameObjectWithTag("shedUI"); not gonna work if it starts toggled inactive, do it manually for now :)
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            shedUI.SetActive(false);
            cam.canPan = true;

        }
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder
        if (Input.GetMouseButtonUp(0))
        {
            shedUI.SetActive(true);
            cam.canPan = false;
        }
    }

    void OnMouseExit()
    {
        mesh.material.color = original;
    }
}
