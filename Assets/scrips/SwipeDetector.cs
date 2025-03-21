using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwiping = false;

    public TrailRenderer trailRenderer;
    public DragAndDrop3D dragAndDrop; // Referencia al script de Drag and Drop

    void Update()
    {
        if (Input.touchCount > 0 && !dragAndDrop.IsDragging()) // Solo detectar swipe si no se está arrastrando un objeto
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
                isSwiping = true;

                // Activar el trail renderer
                trailRenderer.enabled = true;
                trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                fingerUpPosition = touch.position;
                trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                isSwiping = false;
                trailRenderer.enabled = false;

                // Verificar si el swipe es lo suficientemente largo
                if (Vector2.Distance(fingerDownPosition, fingerUpPosition) > 50f)
                {
                    // Eliminar todos los objetos
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
                    foreach (GameObject obj in objects)
                    {
                        Destroy(obj);
                    }
                }
            }
        }
    }
}
