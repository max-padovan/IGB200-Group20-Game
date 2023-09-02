using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class itemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public Item item; //what item this is going to be
    public int count = 0;
    public Text countUI;
    

    public Transform itemParent; //parent to the item being dragged
    public Image image; //the ui element of the item
    public camera cam; //camera script
    public GameObject shedUI; //the ui part relating to shed storage

    //public water waterManager; //unity didn't like getting this

    // Start is called before the first frame update



    public void RefreshCount()
    {
        countUI.text = count.ToString();
        if (item.actionType != Item.ActionType.water) //basically if it's not water, don't display the number if it's 1
        {
            bool textActive = count > 1;
            countUI.gameObject.SetActive(textActive);
        }
    }
    
    public void InitialiseItem(Item newItem)
    {
        
        //image = this.GetComponent<Image>();
        //Debug.Log(newItem);
        item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
    }
    void Start()
    {
        RefreshCount();
        if(item.waterCapacity)
        {
            //count = waterManager.maxWaterCan;
            Debug.Log("I'm water");
        }
        else
        {
            count = 1;
        }
        image = GetComponent<Image>();
        //assigning object references
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        shedUI = GameObject.Find("shedUI");

        //for testing
        //InitialiseItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerDown(PointerEventData pointerEventData) //so when the player clicks on the item, they aren't panning
    {
        cam.canPan = false;
    }
    public void OnBeginDrag(PointerEventData eventData) //for when the player starts dragging the item
    {
        itemParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) //while the player is dragging the item
    {
        transform.position = Input.mousePosition; //just keeping the item sprite on the mouse while it moves
    }

    public void OnEndDrag(PointerEventData eventData) //when the player releases the drag
    {
        transform.SetParent(itemParent); 
        image.raycastTarget = true;
        if(!shedUI.activeInHierarchy)
        {
            cam.canPan = true;
        }
        
    }
}
