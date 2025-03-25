using UnityEngine;

[CreateAssetMenu(fileName = "NewSpriteData", menuName = "Sprite System/Sprite Data")]
public class SpriteDataSO : ScriptableObject
{
    public Sprite sprite;
    public string spriteName;
}