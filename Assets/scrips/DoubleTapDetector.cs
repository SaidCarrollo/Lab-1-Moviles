using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DoubleTapDetector : MonoBehaviour
{
    private float lastTapTime;
    private const float doubleTapTime = 0.3f; // Tiempo máximo entre toques para considerarlo doble tap
    private bool isDoubleTap = false; // Bandera para detectar doble tap
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
        touchControls.Touch.TouchPress.started += ctx => CheckDoubleTap(ctx);
    }

    private void CheckDoubleTap(InputAction.CallbackContext context)
    {
        if (Time.time - lastTapTime < doubleTapTime)
        {
            isDoubleTap = true;

            Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Object"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else
        {
            isDoubleTap = false;
        }
        lastTapTime = Time.time;
    }

    public bool IsDoubleTap()
    {
        return isDoubleTap;
    }
}