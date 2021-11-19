using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHueShifter : HueShifter
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    protected override void HueShift(Color color)
    {
        spriteRenderer.color = color;
    }
}
