using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plantNode : MonoBehaviour
{
    public GameObject selectionPlane;
    private GameObject selectionPlaneInstance;

    private void OnMouseEnter()
    {
        selectionPlaneInstance = Instantiate(selectionPlane, transform.position + new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
    }

    private void OnMouseExit()
    {
        if (selectionPlaneInstance != null)
        {
            Destroy(selectionPlaneInstance);
        }
    }
}
