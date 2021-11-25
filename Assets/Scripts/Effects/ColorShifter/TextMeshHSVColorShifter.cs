using TMPro;
using UnityEngine;

namespace Effects.ColorShifter
{
    public class TextMeshHSVColorShifter : HSVColorShifter
    {
        [Space]
        [SerializeField] TextMeshPro text;

        protected override void ColorShift(Color color)
        {
            text.color = color;
        }
    }
}