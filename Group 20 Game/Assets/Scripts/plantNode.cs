using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plantNode : MonoBehaviour
{
    public GameObject selectionplane;

    void Update()
    {
        ShowSelectionRing();
    }

    private void ShowSelectionRing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                selectionplane.SetActive(true);
                selectionplane.transform.position = hit.point + Vector3.up * 0.05f;
            }
            else
            {
                selectionplane.SetActive(false);
            }
        }
        else
        {
            selectionplane.SetActive(false);
        }
    }
}
