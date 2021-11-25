using UnityEngine;

namespace Effects.ColorShifter
{
    public class MaterialHSVColorShifter : HSVColorShifter
    {
        static readonly int ColorID = Shader.PropertyToID("_Color");

        [Space]
        [SerializeField] Material material;

        protected override void ColorShift(Color color)
        {
            material.SetColor(ColorID, color);
        }
    }
}