using UnityEngine;

public class ParticleHueShifter : HueShifter
{
    [SerializeField] ParticleSystem particle;

    protected override void HueShift(Color color)
    {
        var particleMain = particle.main;
        particleMain.startColor = color;
    }
}
