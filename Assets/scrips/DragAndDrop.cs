using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 offset;
    private bool isDragging = false; // Bandera para saber si estamos arrastrando un objeto

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Object"))
                    {
                        selectedObject = hit.collider.gameObject;
                        offset = selectedObject.transform.position - hit.point;
                        isDragging = true; // Activamos la bandera de arrastre
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject.transform.position = hit.point + offset;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false; // Desactivamos la bandera de arrastre
                selectedObject = null;
            }
        }
    }

    public bool IsDragging()
    {
        return isDragging;
    }
}

