using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LevelHueController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] ColorAdjustments colorAdjustments;
    
    [Space]
    [SerializeField] AffectedMaterial[] affectedMaterials;
    
    float _defaultHue;
    float _defaultSaturation;
    float _defaultValue;

    void Awake()
    {
        volume = GetComponent<Volume>();
        if (volume == null) return;

        volume.profile.TryGet(out colorAdjustments);
        if (colorAdjustments == null) return;

        var color = colorAdjustments.colorFilter.value;
        Color.RGBToHSV(color, out _defaultHue, out _defaultSaturation, out _defaultValue);
    }

    [ContextMenu("RandomHue")]
    public void ChangeToRandomHue()
    {
        if (volume == null || colorAdjustments == null) return;

        var hue = Random.Range(0f, 1f);
        var color = Color.HSVToRGB(hue, _defaultSaturation, _defaultValue);
        colorAdjustments.colorFilter.Override(color);

        if (affectedMaterials != null) ApplyToMaterials(hue);
    }

    void ApplyToMaterials(float hue)
    {
        for (var i = 0; i < affectedMaterials.Length; i++) affectedMaterials[i].Apply(hue);
    }

    [Serializable]
    struct AffectedMaterial
    {
        public Material Material;
        [Range(0f, 1f)] public float Saturation;
        [Range(0f, 1f)] public float Value;

        public void Apply(float hue)
        {
            Material.color = Color.HSVToRGB(hue, Saturation, Value);
        }
    }
}