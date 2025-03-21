using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // Prefab base para crear el objeto
    private Sprite selectedSprite; // Sprite seleccionado
    public Color SelecColor = Color.white;

    public DoubleTapDetector doubleTapDetector; // Referencia al script de doble tap

    // Método para seleccionar el sprite desde el botón
    public void SelectSprite(Image buttonImage)
    {
        selectedSprite = buttonImage.sprite;
    }

    // Método para seleccionar el color desde el botón
    public void SelectColor(Image buttonImage)
    {
        SelecColor = buttonImage.color;
    }

    void Update()
    {
        // Detectar toque en la pantalla
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Verificar si no es un doble tap
            if (!doubleTapDetector.IsDoubleTap())
            {
                // Obtener posición del toque
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPosition.z = 0; // Asegurar que esté en el plano correcto

                // Si hay un sprite seleccionado, crear el objeto con el color previamente elegido
                if (selectedSprite != null)
                {
                    GameObject newObject = Instantiate(prefab, touchPosition, Quaternion.identity);
                    SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();

                    sr.sprite = selectedSprite;  // Asignar sprite
                    sr.color = SelecColor;    // Aplicar el color previamente seleccionado

                    // Asignar un tag para identificar los objetos creados
                    newObject.tag = "Object";
                }
            }
        }
    }
}
