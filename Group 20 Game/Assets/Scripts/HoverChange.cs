using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverChange : MonoBehaviour
{
    MeshRenderer mesh;
    Material material;
    Color Original;
    Color Hover = Color.gray;
    public camera cam;

    void OnMouseOver()
    {
        if (cam.canPan)
        {
            this.material.color = Hover;
        }
        
    }

    void OnMouseExit()
    {
        this.material.color = Original;
    }


        // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
        Original = material.color;
        //Original = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
