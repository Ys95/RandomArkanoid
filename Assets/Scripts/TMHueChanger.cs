using TMPro;
using UnityEngine;

public class TMHueChanger : HueChanger
{
    [SerializeField] TextMeshPro text;
    
    protected override void HueShift(Color color)
    {
        text.color = color;
    }
}
