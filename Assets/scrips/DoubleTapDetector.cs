using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapDetector : MonoBehaviour
{
    private float lastTapTime;
    private const float doubleTapTime = 0.3f; // Tiempo máximo entre toques para considerarlo doble tap
    private bool isDoubleTap = false; // Bandera para detectar doble tap

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Time.time - lastTapTime < doubleTapTime)
            {
                // Doble tap detectado
                isDoubleTap = true;

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPosition.z = 0;

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit))
                {
                    if (hit.collider.CompareTag("Object")) // Asegúrate de que el objeto tenga el tag "Object"
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                // No es un doble tap
                isDoubleTap = false;
            }
            lastTapTime = Time.time;
        }
    }

    public bool IsDoubleTap()
    {
        return isDoubleTap;
    }
}
