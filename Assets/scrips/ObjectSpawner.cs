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
    public SpriteDataSO currentSpriteData;
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

    // Método para seleccionar sprite (desde botón UI)
    //Viejo
    //public void SelectSprite(Image buttonImage)
    //{
    //    selectedSprite = buttonImage.sprite;
    //}
    public void SelectSprite(SpriteDataSO spriteData)
    {
        currentSpriteData = spriteData;
    }
    //// Método original para selección desde UI (opcional)
    //public void SelectSpriteFromImage(Image buttonImage)
    //{
    //    if (currentSpriteData != null && buttonImage.sprite != null)
    //    {
    //        currentSpriteData.sprite = buttonImage.sprite;
    //    }
    //}
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
    //Viejo
    //private void TrySpawnObject(InputAction.CallbackContext context)
    //{
    //    if (doubleTapDetector != null && doubleTapDetector.IsDoubleTap()) return;
    //    if (selectedSprite == null || currentColorData == null) return;

    //    Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
    //    worldPosition.z = 0;

    //    GameObject newObject = Instantiate(prefab, worldPosition, Quaternion.identity);
    //    SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
    //    sr.sprite = selectedSprite;
    //    sr.color = currentColorData.colorValue; // Usamos el color del SO
    //    newObject.tag = "Object";
    //}
    //nuevo
    private void TrySpawnObject(InputAction.CallbackContext context)
    {
        if (doubleTapDetector != null && doubleTapDetector.IsDoubleTap()) return;
        if (currentSpriteData == null || currentColorData == null) return;

        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
        worldPosition.z = 0;

        GameObject newObject = Instantiate(prefab, worldPosition, Quaternion.identity);
        SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
        sr.sprite = currentSpriteData.sprite; // Usamos el sprite del SO
        sr.color = currentColorData.colorValue;
        newObject.tag = "Object";
    }

    void OnDestroy()
    {
        touchControls.Disable();
    }
}