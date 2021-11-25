using UnityEngine;

namespace Effects.ColorShifter
{
    public class SpriteHSVColorShifter : HSVColorShifter
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        protected override void ColorShift(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}