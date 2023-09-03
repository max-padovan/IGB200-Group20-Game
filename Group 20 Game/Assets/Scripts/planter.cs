using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Grid grid;
    public Vector3 nodePosition;
    private bool isFirstPlace = true;
    private List<Vector3> nodePositions = new List<Vector3>();

    private Vector3 pos;

    //Seed Variables ----------------
    public GameObject testSeeds;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        mesh = GetComponent<MeshRenderer>();

        prepareBuildingPrefab();
    }

    private void Update()
    {
        if (camOverPlanter == true && mouseOverPlanter == true)
        {
            showMouseIndicator();

            Item activeItem = inventoryManager.QuerySelectedItem(false);
            //Item shovel = inventoryManager.GetItemRef(1); //this number will change
            if (activeItem != null)
            {
                if (activeItem.actionType == Item.ActionType.dig)
                {
                    placePlantNode();
                }

                if (activeItem.actionType == Item.ActionType.plant)
                {
                    placeTestSeed();
                }

                if (activeItem.actionType == Item.ActionType.water)
                {
                    testSeeds.GetComponent<TestPlant>().waterCrop();
                }
            }
        }

        camOverPlanter = !cam.isHome; //isHome is managed by the camera script and is changed there...
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder

        if (Input.GetMouseButtonUp(0) && cam.canPan)
        {
            Debug.Log("Clicked planter!");

            //call function that moves camera to this location
            Cursor.visible = false;
            cam.GetComponent<camera>().Move(this.transform.position, true);
        }

        mouseOverPlanter = true;
    }

    void OnMouseExit()
    {
        mesh.material.color = original;
        mouseOverPlanter = false;
        placeholderPlantNode.SetActive(false);
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

    public void showMouseIndicator()
    {
        placeholderPlantNode.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Convert world position of palceHolderPlantNode to grid position (snaps to grid)
            Vector3Int gridPosition = grid.WorldToCell(hit.point);
            placeholderPlantNode.transform.position = grid.CellToWorld(gridPosition) + placeHolderPlantNodeOffset; // <- provides a little offset to see where the "cursor" is
        }
    }

    public void placePlantNode()
    {
        /*
        if (Input.GetMouseButtonDown(0) && isFirstPlace == true)
        {
            pos = new Vector3(placeholderPlantNode.transform.position.x,
                                placeholderPlantNode.transform.position.y - 0.3f,
                                placeholderPlantNode.transform.position.z);
            Instantiate(plantNode, pos, Quaternion.identity);

            isFirstPlace = false;
        }
                    
        if (Input.GetMouseButtonDown(0) && isFirstPlace == false)
        {
            pos = new Vector3(placeholderPlantNode.transform.position.x,
                                placeholderPlantNode.transform.position.y - 0.3f,
                                placeholderPlantNode.transform.position.z);

            List<Vector3> positionsToAdd = new List<Vector3>();
            nodePositions.Add(pos);

            foreach (Vector3 item in nodePositions)
            {
                float distance = Vector3.Distance(item, pos);

                if (distance > 0.5f)
                {
                    positionsToAdd.Add(pos);
                    nodePositions.AddRange(positionsToAdd);
                    Instantiate(plantNode, pos, Quaternion.identity);
                }
            }
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            pos = new Vector3(placeholderPlantNode.transform.position.x,
                                placeholderPlantNode.transform.position.y - 0.3f,
                                placeholderPlantNode.transform.position.z);
            Instantiate(plantNode, pos, Quaternion.identity);
        }
    }

    public void placeTestSeed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //places an instance of plant where there is a plant node
            //plantNode.GetComponent<plantNodeScript>().placeSeeds(testSeeds);

            pos = new Vector3(placeholderPlantNode.transform.position.x,
                                placeholderPlantNode.transform.position.y - 0.3f,
                                placeholderPlantNode.transform.position.z);
            Instantiate(testSeeds, pos, Quaternion.identity);
        }
    }
}