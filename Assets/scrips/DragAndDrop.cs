using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DragAndDrop3D : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 offset;
    private bool isDragging = false; // Bandera para saber si estamos arrastrando un objeto
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
        touchControls.Touch.TouchPress.started += ctx => StartDrag(ctx);
        touchControls.Touch.TouchPosition.performed += ctx => ContinueDrag(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndDrag(ctx);
    }

    private void StartDrag(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Object"))
            {
                selectedObject = hit.collider.gameObject;
                offset = selectedObject.transform.position - hit.point;
                isDragging = true;
            }
        }
    }

    private void ContinueDrag(InputAction.CallbackContext context)
    {
        if (!isDragging) return;

        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selectedObject.transform.position = hit.point + offset;
        }
    }

    private void EndDrag(InputAction.CallbackContext context)
    {
        isDragging = false;
        selectedObject = null;
    }

    public bool IsDragging()
    {
        return isDragging;
    }
}