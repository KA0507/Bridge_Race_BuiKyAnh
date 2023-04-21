using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3,
    Purple = 4,
    Gray = 5
}

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] private Material[] materials;

    public Material GetMaterial(ColorType colorType)
    {
        return materials[(int)colorType];
    }
}
