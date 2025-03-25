using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public SpriteDataSO spriteData;
    public ObjectSpawner spawner;

    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (spriteData != null && spriteData.sprite != null)
        {
            buttonImage.sprite = spriteData.sprite;
        }

        button.onClick.AddListener(OnSpriteSelected);
    }

    void OnSpriteSelected()
    {
        if (spawner != null && spriteData != null)
        {
            spawner.SelectSprite(spriteData);
        }
    }
}