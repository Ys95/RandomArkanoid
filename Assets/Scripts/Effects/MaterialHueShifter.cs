using UnityEngine;

public class MaterialHueShifter : HueShifter
{
    [Space]
    [SerializeField] Material material;
    static readonly int ColorID = Shader.PropertyToID("_Color");

    protected override void HueShift(Color color)
    {
        material.SetColor(ColorID,color);
    }
}
