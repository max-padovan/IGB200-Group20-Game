using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class invSlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColour;
    public Color notSelectedColour;



    public void Select()
    {
        image.color = selectedColour;
    }
    public void Deselect()
    {
        image.color = notSelectedColour;
    }

    public void OnDrop(PointerEventData eventData) //when an item is dropped onto this slot
    {
        if(transform.childCount ==0)
        {
            GameObject dropped = eventData.pointerDrag;
            itemDrag draggable = dropped.GetComponent<itemDrag>();
            draggable.itemParent = transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
