using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LevelHueController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] ColorAdjustments colorAdjustments;

    float _defaultHue;
    float _defaultSaturation;
    float _defaultValue;
    
    void Awake()
    {
        volume = GetComponent<Volume>();
        if(volume==null) return;
        
        volume.profile.TryGet(out colorAdjustments);
        if (colorAdjustments == null) return;
        
        Color color = colorAdjustments.colorFilter.value;
        Color.RGBToHSV(color, out _defaultHue,  out _defaultSaturation, out _defaultValue);
    }

    [ContextMenu("RandomHue")]
    public void ChangeToRandomHue()
    {
        if(volume  ==null|| colorAdjustments==null) return;
        
        float hue = Random.Range(0f, 1f);
        Color color = Color.HSVToRGB(hue, _defaultSaturation, _defaultValue);
        colorAdjustments.colorFilter.Override(color);
    }
    
}

