using UnityEngine;

public class SpriteHSVColorShifter : HSVColorShifter
{
    [SerializeField] SpriteRenderer spriteRenderer;

    protected override void ColorShift(Color color)
    {
        spriteRenderer.color = color;
    }
}