using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public class planter : MonoBehaviour
{
    //Selection variables when isHome == true -----------------
    MeshRenderer mesh;
    Color original = Color.white; //get component of its mesh ig
    Color hoverCol = Color.gray;
    private camera cam;

    //Inventory variables ----------------------------------------
    public int shovelID = 1; //the current ID for the digging tool
    public InventoryManager inventoryManager;

    //Grid variables ----------------------------
    private bool camOverPlanter = false;
    private bool mouseOverPlanter = false;
    private bool mouseOverGridCube = false;
    public GridCheck gridcheck;

    public GameObject plantNode;
    private GameObject placeholderPlantNode;

    public GameObject plantPrefab;

    public Material validPlacementMaterial;
    private Vector3 placeHolderPlantNodeOffset = new Vector3(0.8f, -0.5f, 1.2f);

    public Grid grid;
    public Vector3 nodePosition;
    
    private List<Vector3> nodePositions = new List<Vector3>();

    private Vector3 pos;

    //Seed Variables ----------------
    public GameObject testSeeds;

    //public int[] xGridPlaced;
    //public int[] zGridPlaced;
    private bool hasSoilPlaced = false;
    private bool hasSeedPlaced = false;
    public List<float> xGridPlaced = new List<float>();
    public List<float> zGridPlaced = new List<float>();

    public List<float> xGridSeeded = new List<float>();
    public List<float> zGridSeeded = new List<float>();

    private List<GameObject> nodesPlaced = new List<GameObject>(); 
    private List<GameObject> plantsPlaced = new List<GameObject>();
    public int nodesPlacedNum = 0;
    public int plantsPlacedNum = 0;

    public int PlanterType; //0 for sun, 1 for half, 2 for shade

    public Notification notification;
    public water waterManager;


    //audio stuff
    public AudioSource waterSound;
    public AudioSource digSound;
    public AudioSource harvestSound;
    public AudioSource errorSound;
    public AudioSource removeSound;

    void Start()
    {
        notification = GameObject.Find("NotificationManager").GetComponent<Notification>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        mesh = GetComponent<MeshRenderer>();
        //original = mesh.GetComponent<Color>();

        prepareBuildingPrefab();
    }



    private void PlantSeed(plantInfo plantinfo)
    {
        if (Input.GetMouseButtonDown(0))
        {
            float currentxPos = placeholderPlantNode.transform.position.x;
            float currentzPos = placeholderPlantNode.transform.position.z;
            float currentyPos = placeholderPlantNode.transform.position.y;
            bool isSoil = false;
            bool canPlant = true;

            //Check first if it's got soil
            for (int x = 0; xGridPlaced.Count > x; x++)
            {
                //this only works if the grid is square?
                if (xGridPlaced[x] == currentxPos && zGridPlaced[x] == currentzPos)// Yes, there's soil
                {
                    isSoil = true;
                    break;
                }

            }


            if (isSoil)
            {

                //check for seed already there
                for (int x = 0; xGridSeeded.Count > x; x++)
                {
                    //this only works if the grid is square?
                    if (xGridSeeded[x] == currentxPos && zGridSeeded[x] == currentzPos)
                    {
                        //there's already a seed there
                        //because we can only add the location to the array when the player puts it in
                        canPlant = false;
                        break;

                    }

                }
                if (canPlant)
                {
                    Debug.Log("should remove 1 from inv");
                    inventoryManager.QuerySelectedItem(true); //this isn't working properly and is removing ALL of the seeds in the stack
                    pos = new Vector3(currentxPos, currentyPos - 0.3f, currentzPos);
                    //plantsPlaced.Add(Instantiate(plantPrefab, pos, Quaternion.identity));
                    GameObject newSeed = Instantiate(plantPrefab, pos, Quaternion.identity);
                    //place the seed there, then add it to the gridSeeded lists so it won't be placed there again
                    //GameObject newSeed = Instantiate(plantPrefab, pos, Quaternion.identity); //spawn in a generic plant object
                    plant plantComponent = newSeed.GetComponent<plant>(); //get plant component of that specific new object so we can change things
                    plantComponent.plantinfo = plantinfo; //set the plantinfo of this newly instantiated plant to the one corresponding to the seed used to plant it
                    if(plantinfo.lightRequirement == PlanterType)
                    {
                        plantComponent.rightPlanter = true;
                    }
                    else
                    {
                        plantComponent.rightPlanter = false;
                    }
                    //Instantiate(plantNode, pos, Quaternion.identity);
                    //plantsPlaced[plantsPlacedNum].GetComponent<plant>();
                    plantsPlaced.Add(newSeed);
                    plantsPlacedNum += 1;
                    xGridSeeded.Add(currentxPos);
                    zGridSeeded.Add(currentzPos);
                }
            }
            //places an instance of plant where there is a plant node
            //plantNode.GetComponent<plantNodeScript>().placeSeeds(testSeeds);
        }

        
       
    }

    private void waterOnce()
    {
        
        int layer = 3;
        int layerMask = 1 << layer;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            //bool watered = waterManager.UseWater(1);
            if (waterManager.UseWater(1))
            {
                waterSound.Play();
                Debug.Log(hit.transform.name);
                plant wateringPlant = hit.transform.GetComponent<plant>();
                wateringPlant.amountWateredToday += 1;
                //notification.notif("Added 1 water to this plant");
            }

        }
        else
        {
            notification.notif("You can only water the base of plants");
            errorSound.Play();
        }
    }

    private void Update()
    {
        mouseOverGridCube = gridcheck.isOverGrid;
        //Debug.Log(mouseOverGridCube);
        if (camOverPlanter == true && mouseOverGridCube) // && mouseOverPlanter == true
        {
            //Debug.Log("yep");
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
                    PlantSeed(activeItem.plant);
                    //placeTestSeed();

                }

                if (activeItem.actionType == Item.ActionType.water)
                {
                    //testSeeds.GetComponent<TestPlant>().waterCrop();
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        waterOnce();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    notification.notif("You aren't holding a tool or seed");
                }
                
            }
        }

        camOverPlanter = !cam.isHome; //isHome is managed by the camera script and is changed there...
    }

    void OnMouseOver()
    {
        mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder
        //Debug.Log(cam.canPan);
        if (Input.GetMouseButtonUp(0) && cam.canPan)
        {
            //Debug.Log("Clicked planter!");

            //call function that moves camera to this location

            //Cursor.visible = false; //ambiguous mention of Cursor? just commented out for now
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
        if (Input.GetMouseButtonDown(0))
        {
            //get the current position
            float currentxPos = placeholderPlantNode.transform.position.x;
            float currentzPos = placeholderPlantNode.transform.position.z;
            float currentyPos = placeholderPlantNode.transform.position.y;
            bool CanPlace = true;
            bool isSeed = false;

            //This is a fairly inefficient way of doing it
            for (int x = 0;xGridPlaced.Count > x;x++)
            {
                if (xGridPlaced[x] == currentxPos && zGridPlaced[x] == currentzPos) //if we found the node
                {
                    for (int xx = 0; xGridSeeded.Count > xx; xx++)
                    {
                        if (xGridSeeded[xx] == currentxPos && zGridSeeded[xx] == currentzPos) //if we found the seed
                        {
                            isSeed = true; //not sure if I'll use this
                            //there's already a seed there
                            //remove the seed there
                            plant plantRef = plantsPlaced[xx].GetComponent<plant>(); //reference to the script of the plant in this location
                            ///0 = seedling
                            ///1 = young + healthy
                            ///2 = young + unhealthy
                            ///3 = mature + healthy
                            ///4 = mature + unhealthy
                            ///5 = fruiting
                            ///6 = dead
                            if (plantRef.currentState == 0 || plantRef.currentState == 5 || plantRef.currentState == 6)
                            {
                                harvestSound.Play(); //removing / harvesting
                                Debug.Log(plantRef.currentState);
                                if(plantRef.currentState == 5)
                                {
                                    //give produce depending on health
                                    plantRef.harvestProduce();
                                }
                                
                                //it's in a state we can clear
                                Destroy(plantsPlaced[xx]);
                                plantsPlaced.RemoveAt(xx);
                                xGridSeeded.RemoveAt(xx);
                                zGridSeeded.RemoveAt(xx);
                                Debug.Log("DESTROY PLANT");
                                plantsPlacedNum--;

                                //clear the node too
                                Destroy(nodesPlaced[x]);
                                nodesPlaced.RemoveAt(x);
                                xGridPlaced.RemoveAt(x);
                                zGridPlaced.RemoveAt(x);
                                Debug.Log("DESTROY SOIL");
                                
                                nodesPlacedNum--;
                                //break;
                            }
                            CanPlace = false;
                            //break;
                            //it's not in a state we can clear
                        }
                    }
                    //there's already a soil there
                    //because we can only add the location to the array when the player puts it in
                    if(!isSeed) //no seed, relax
                    {
                        removeSound.Play();
                        Destroy(nodesPlaced[x]);
                        nodesPlaced.RemoveAt(x);
                        xGridPlaced.RemoveAt(x);
                        zGridPlaced.RemoveAt(x);
                        Debug.Log("DESTROY SOIL");
                        CanPlace = false;
                        nodesPlacedNum--;
                        break;
                    }


                }
            }
            if(CanPlace)
            {
                //place the soil there, then add it to the gridPlaced lists so it won't be placed there again
                pos = new Vector3(currentxPos, currentyPos - 0.3f, currentzPos);
                nodesPlaced.Add(Instantiate(plantNode, pos, Quaternion.identity));
                nodesPlacedNum += 1;
                xGridPlaced.Add(currentxPos);
                zGridPlaced.Add(currentzPos);
                digSound.Play();
            }
        }
    }

    public void placeTestSeed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float currentxPos = placeholderPlantNode.transform.position.x;
            float currentzPos = placeholderPlantNode.transform.position.z;
            float currentyPos = placeholderPlantNode.transform.position.y;
            bool isSoil = false;
            bool canPlant = true;

            //Check first if it's got soil
            for (int x = 0; xGridPlaced.Count > x; x++)
            {
                //this only works if the grid is square?
                if (xGridPlaced[x] == currentxPos && zGridPlaced[x] == currentzPos)// Yes, there's soil
                {
                    isSoil = true;
                    break;
                }
                
            }


            if (isSoil)
            {
                
                //check for seed already there
                for (int x = 0; xGridSeeded.Count > x; x++)
                {
                    //this only works if the grid is square?
                    if (xGridSeeded[x] == currentxPos && zGridSeeded[x] == currentzPos)
                    {
                        //there's already a seed there
                        //because we can only add the location to the array when the player puts it in
                        canPlant = false;
                        break;

                    }

                }
                if (canPlant)
                {
                    //place the seed there, then add it to the gridSeeded lists so it won't be placed there again
                    pos = new Vector3(currentxPos, currentyPos - 0.3f, currentzPos);
                    Instantiate(testSeeds, pos, Quaternion.identity);
                    xGridSeeded.Add(currentxPos);
                    zGridSeeded.Add(currentzPos);
                }
            }
            //places an instance of plant where there is a plant node
            //plantNode.GetComponent<plantNodeScript>().placeSeeds(testSeeds);
        }
        //get the current position


        


        
    }
}