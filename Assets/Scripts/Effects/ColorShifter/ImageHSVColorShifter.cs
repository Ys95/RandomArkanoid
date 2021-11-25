using UnityEngine;
using UnityEngine.UI;

namespace Effects.ColorShifter
{
    public class ImageHSVColorShifter : HSVColorShifter
    {
        [SerializeField] Image image;

        protected override void ColorShift(Color color)
        {
            image.color = color;
        }
    }
}