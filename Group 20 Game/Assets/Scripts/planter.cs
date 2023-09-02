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
    private Vector3 placeHolderPlantNodeOffset = new Vector3(0, 0.7f, 0);

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
            Cursor.visible = false;
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
        //Item shovel = inventoryManager.GetItemRef(1); //this number will change

        if (activeItem != null) // just in case they aren't holding anything at all - avoid the error
        {
            //if the player has selected the planter (cam over it), has their mouse over the object (mouse over) and their tool has the dig type
            if (camOverPlanter == true && mouseOverPlanter == true && activeItem.actionType == Item.ActionType.dig)
            {
                placeholderPlantNode.SetActive(true);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    nodePosition = hit.point;

                    //places an actual plantNode
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(plantNode, new Vector3(placeholderPlantNode.transform.position.x,
                                                           placeholderPlantNode.transform.position.y - 0.3f,
                                                           placeholderPlantNode.transform.position.z),
                                                           Quaternion.identity);
                    }
                }
            }
            else
            {
                placeholderPlantNode.SetActive(false);
            }
        }
    }

    public void gridManager()
    {
        Vector3Int gridPosition = grid.WorldToCell(nodePosition);
        placeholderPlantNode.transform.position = grid.CellToWorld(gridPosition) + placeHolderPlantNodeOffset;
    }
}