using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // Prefab base para crear el objeto
    private Sprite selectedSprite; // Sprite seleccionado
    public ColorDataSO currentColorData; // Reemplaza el Color directo
    public SpriteDataSO currentSriteData;
    public DoubleTapDetector doubleTapDetector; // Referencia al script de doble tap
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
        touchControls.Touch.TouchPress.started += ctx => TrySpawnObject(ctx);
    }

    public void SelectSprite(Image buttonImage)
    {
        selectedSprite = buttonImage.sprite;
    }

    public void SelectColor(ColorDataSO colorData)
    {
        currentColorData = colorData;
    }
    public void SelectColorFromUI(Image buttonImage)
    {
        if (currentColorData != null)
        {
            currentColorData.colorValue = buttonImage.color;
        }
    }
    private void TrySpawnObject(InputAction.CallbackContext context)
    {
        if (doubleTapDetector != null && doubleTapDetector.IsDoubleTap()) return;
        if (selectedSprite == null || currentColorData == null) return;

        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
        worldPosition.z = 0;

        GameObject newObject = Instantiate(prefab, worldPosition, Quaternion.identity);
        SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
        sr.sprite = selectedSprite;
        sr.color = currentColorData.colorValue; // Usamos el color del SO
        newObject.tag = "Object";
    }
}