using TMPro;
using UnityEngine;

public class TextMeshHueShifter : HueShifter
{
    [Space]
    [SerializeField] TextMeshPro text;
    
    protected override void HueShift(Color color)
    {
        text.color = color;
    }
}
