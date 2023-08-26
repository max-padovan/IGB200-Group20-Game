using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class invSlot : MonoBehaviour, IDropHandler
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
