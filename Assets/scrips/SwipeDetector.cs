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
    public ObjectSpawner objectSpawner; // Referencia al script de ObjectSpawner
    private Material trailMaterial; // Material dinámico para el TrailRenderer

    void Start()
    {
        // Crear un material dinámico con el shader Unlit/Color
        trailMaterial = new Material(Shader.Find("Unlit/Color"));
        trailRenderer.material = trailMaterial; // Asignar el material al TrailRenderer

        // Asegurarse de que el TrailRenderer esté desactivado al inicio
        trailRenderer.enabled = false;

        // Asignar el color inicial del TrailRenderer
        if (objectSpawner != null)
        {
            UpdateTrailColor();
        }
    }

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

                // Activar el trail renderer y configurar su posición
                trailRenderer.enabled = true;
                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                trailRenderer.transform.position = touchWorldPosition;

                // Asignar el color seleccionado al TrailRenderer
                UpdateTrailColor();

                // Limpiar el trail anterior
                trailRenderer.Clear();
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                fingerUpPosition = touch.position;
                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                trailRenderer.transform.position = touchWorldPosition;
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

    // Método para actualizar el color del TrailRenderer
    private void UpdateTrailColor()
    {
        if (objectSpawner != null && trailMaterial != null)
        {
            // Cambiar el color del material dinámico
            trailMaterial.color = objectSpawner.SelecColor;
        }
    }
}
