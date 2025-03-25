
// ColorDataSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorData", menuName = "Color System/Color Data")]
public class ColorDataSO : ScriptableObject
{
    public Color colorValue;
    public string colorName;
    public Material trailMaterialPrefab; // Opcional: material asociado
}