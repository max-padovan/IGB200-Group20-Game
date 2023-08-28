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

    public int shovelID = 1; //the current ID for the digging tool

    public InventoryManager inventoryManager;


    public bool insidePlanter = false;
    public GameObject plantNode;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        cam = GameObject.Find("Main Camera");
        mesh = GetComponent<MeshRenderer>();
        Debug.Log(original);
    }

    private void Update()
    {
        placePlantNode();
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Clicked planter!");
            
            //call function that moves camera to this location
            cam.GetComponent<camera>().Move(this.transform.position,true);

            insidePlanter = true;
        }
    }

    void OnMouseExit()
    {
        mesh.material.color = original;
        insidePlanter = false;
    }

    void placePlantNode()
    {
        Item activeItem = inventoryManager.QuerySelectedItem(false);
        Item shovel = inventoryManager.GetItemRef(1); //this number will change

        if (Input.GetMouseButtonDown(0) && insidePlanter == true && activeItem == shovel)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 nodePosition = hit.point;
                Instantiate(plantNode, nodePosition, Quaternion.identity);
            }
        }
    }
}