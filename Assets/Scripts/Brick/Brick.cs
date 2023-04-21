using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ColorData cd;

    private int brickColor = -1;
    
    public void SetBrickColor(int color)
    {
        brickColor = color;
        meshRenderer.material = cd.GetMaterial((ColorType)color);
    }
    public int BrickColor
    {
        get { return brickColor; }
        set { brickColor = value; }
    }
}
