using System;
using UnityEngine;

public class MaterialHSVColorShifter : HSVColorShifter
{
    [Space]
    [SerializeField] Material material;
    static readonly int ColorID = Shader.PropertyToID("_Color");
    
    protected override void ColorShift(Color color)
    {
        material.SetColor(ColorID,color);
    }
}
