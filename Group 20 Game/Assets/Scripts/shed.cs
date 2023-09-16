using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shed : MonoBehaviour
{
    //MeshRenderer mesh;
    //Color original = Color.white;
    //Color hoverCol = Color.gray;
    public GameObject UI;
    public camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            UI.SetActive(false);
            cam.canPan = true;

        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && cam.canPan)
        {
            UI.SetActive(true);
            cam.canPan = false;
        }
    }
}
