using UnityEngine;

public class ParticleHueChanger : HueChanger
{
    [SerializeField] ParticleSystem particle;

    protected override void HueShift(Color color)
    {
        var particleMain = particle.main;
        particleMain.startColor = color;
    }
}
