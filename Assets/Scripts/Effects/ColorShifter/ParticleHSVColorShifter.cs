using UnityEngine;

namespace Effects.ColorShifter
{
    public class ParticleHSVColorShifter : HSVColorShifter
    {
        [SerializeField] ParticleSystem particle;

        protected override void ColorShift(Color color)
        {
            var particleMain = particle.main;
            particleMain.startColor = color;
        }
    }
}