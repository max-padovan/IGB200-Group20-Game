using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class itemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public Transform itemParent; //parent to the item being dragged
    public Image item; //the ui element of the item
    public camera cam; //camera script
    public GameObject shedUI; //the ui part relating to shed storage

    // Start is called before the first frame update
    void Start()
    {
        //assigning object references
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        shedUI = GameObject.Find("shedUI");
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
        item.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) //while the player is dragging the item
    {
        transform.position = Input.mousePosition; //just keeping the item sprite on the mouse while it moves
    }

    public void OnEndDrag(PointerEventData eventData) //when the player releases the drag
    {
        transform.SetParent(itemParent); 
        item.raycastTarget = true;
        if(!shedUI.activeInHierarchy)
        {
            cam.canPan = true;
        }
        
    }
}
