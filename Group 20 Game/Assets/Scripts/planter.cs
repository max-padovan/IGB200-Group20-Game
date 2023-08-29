using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class planter : MonoBehaviour
{
    //Selection variables when isHome == true -----------------
    MeshRenderer mesh;
    Color original = Color.white;
    Color hoverCol = Color.gray;
    private camera cam;

    //Inventory variables ----------------------------------------
    public int shovelID = 1; //the current ID for the digging tool
    public InventoryManager inventoryManager;

    //Grid variables ----------------------------
    private bool camOverPlanter = false;
    private bool mouseOverPlanter = false;
    public GameObject plantNode;
    private GameObject placeholderPlantNode;

    public Material validPlacementMaterial;

    public Vector3 nodePosition;
    public Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        mesh = GetComponent<MeshRenderer>();
        Debug.Log(original);

        prepareBuildingPrefab();
    }

    private void Update()
    {
        placePlantNode();
        gridManager();
        
        //isHome is managed by the camera script and is changed there...
        camOverPlanter = !cam.isHome;
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Clicked planter!");
            
            //call function that moves camera to this location
            cam.GetComponent<camera>().Move(this.transform.position,true);
        }

        mouseOverPlanter = true;
    }

    void OnMouseExit()
    {
        mesh.material.color = original;
        mouseOverPlanter = false;
    }

    private void prepareBuildingPrefab() // lets the player move the plantNode or visula feedback
    {
        if (placeholderPlantNode)
        {
            Destroy(placeholderPlantNode);
        }
        
        placeholderPlantNode = Instantiate(plantNode);

        //set the colour of the plant node when first created
        placeholderPlantNode.GetComponentInChildren<MeshRenderer>().material = validPlacementMaterial;

        placeholderPlantNode.SetActive(false);
    }

    public void placePlantNode()
    {
        Item activeItem = inventoryManager.QuerySelectedItem(false);
        Item shovel = inventoryManager.GetItemRef(1); //this number will change

        if (camOverPlanter == true && mouseOverPlanter == true && activeItem == shovel)
        {
            placeholderPlantNode.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                nodePosition = hit.point;

                //Update the position of the visual rep of the plantNode
                //placeholderPlantNode.transform.position = nodePosition;
                
                //places an actual plantNode
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(plantNode, nodePosition, Quaternion.identity);
                }
            }
        }
        else
        {
            placeholderPlantNode.SetActive(false);
        }
    }

    public void gridManager()
    {
        Vector3Int gridPosition = grid.WorldToCell(nodePosition);
        placeholderPlantNode.transform.position = grid.CellToWorld(gridPosition);
    }
}