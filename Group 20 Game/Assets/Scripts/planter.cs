using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class planter : MonoBehaviour
{
    MeshRenderer mesh;
    Color original = Color.white;
    Color hoverCol = Color.gray;
    public GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        mesh = GetComponent<MeshRenderer>();
        Debug.Log(original);
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked planter!");
            
            //call function that moves camera to this location
            cam.GetComponent<camera>().Move(this.transform.position,true);
        }
    }

    void OnMouseExit()
    {
        mesh.material.color = original;
    }



}
