using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwiping = false;

    public TrailRenderer trailRenderer;
    public DragAndDrop3D dragAndDrop; // Referencia al script de Drag and Drop
    public ObjectSpawner objectSpawner; // Referencia al script de ObjectSpawner
    private Material trailMaterial; // Material dinámico para el TrailRenderer

    private PlayerInputActions touchControls;

    void Awake()
    {
        touchControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        touchControls.Enable();
    }

    void OnDisable()
    {
        touchControls.Disable();
    }

    void Start()
    {
        trailMaterial = new Material(Shader.Find("Unlit/Color"));
        trailRenderer.material = trailMaterial;
        trailRenderer.enabled = false;

        if (objectSpawner != null)
        {
            UpdateTrailColor();
        }

        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPosition.performed += ctx => MoveTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (dragAndDrop != null && dragAndDrop.IsDragging()) return;

        fingerDownPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        fingerUpPosition = fingerDownPosition;
        isSwiping = true;

        trailRenderer.enabled = true;
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(fingerDownPosition.x, fingerDownPosition.y, 10));
        trailRenderer.transform.position = touchWorldPosition;
        UpdateTrailColor();
        trailRenderer.Clear();
    }

    private void MoveTouch(InputAction.CallbackContext context)
    {
        if (!isSwiping) return;

        fingerUpPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(fingerUpPosition.x, fingerUpPosition.y, 10));
        trailRenderer.transform.position = touchWorldPosition;
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (!isSwiping) return;

        isSwiping = false;
        trailRenderer.enabled = false;

        if (Vector2.Distance(fingerDownPosition, fingerUpPosition) > 50f)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
    }

    private void UpdateTrailColor()
    {
        if (objectSpawner != null && trailMaterial != null)
        {
            trailMaterial.color = objectSpawner.SelecColor;
        }
    }
}