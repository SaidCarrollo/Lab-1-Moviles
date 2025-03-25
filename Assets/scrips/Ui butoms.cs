using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uibutoms : MonoBehaviour
{
    public ColorDataSO colorData;
    public ObjectSpawner spawner;

    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (colorData != null)
        {
            buttonImage.color = colorData.colorValue;
        }

        button.onClick.AddListener(OnColorSelected);
    }

    void OnColorSelected()
    {
        if (spawner != null && colorData != null)
        {
            spawner.SelectColor(colorData);
        }
    }
}
