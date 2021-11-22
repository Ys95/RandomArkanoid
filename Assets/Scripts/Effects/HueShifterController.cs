using System;
using System.Collections;
using UnityEngine;

public class HueShifterController : MonoBehaviour
{
    [SerializeField] HSVColor hsvColor;
    [SerializeField] Shifted hue;
    [SerializeField] Shifted value;
    [SerializeField] Shifted saturation;

    [Space]
    [Range(0f, 1f)] public float tickFrequency;

    public Action<Color> ColorShift;

    [System.Serializable]
    struct HSVColor
    {
        [Range(0f, 1f)] public float h;
        [Range(0f, 1f)] public float s;
        [Range(0f, 1f)] public float v;

        public HSVColor(float h, float s, float v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }
    }

    [Serializable]
    struct Shifted
    {
        public bool shift;
        [Range(0f, 1f)] public float amountPerTick;
        
        [Space]
        [Range(0f, 1f)] public float min;
        [Range(0f, 1f)] public float max;

        bool _reverse;
        
        public float Shift(float hsv)
        {
            if (!_reverse)
            {
                hsv += amountPerTick;
                if (hsv >= max)
                {
                    _reverse = true;
                    return hsv;
                }
            }
            else
            {
                hsv -= amountPerTick;
                if (hsv <= min)
                {
                    _reverse = false;
                    return hsv;
                }
            }
            return hsv;
        }
    }
    
    void OnEnable()
    {
        if(hue.shift) hsvColor.h = UnityEngine.Random.Range(0f, 1f);
        StartCoroutine(ColorShiftCoroutine());
    }

    Color GetColor()
    {
        if (hue.shift) hsvColor.h = hue.Shift(hsvColor.h);
        if (saturation.shift) hsvColor.s = saturation.Shift(hsvColor.s);
        if (value.shift) hsvColor.v = value.Shift(hsvColor.v);

        return Color.HSVToRGB(hsvColor.h, hsvColor.s, hsvColor.v);
    }

    IEnumerator ColorShiftCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            ColorShift?.Invoke(GetColor());
            yield return new WaitForSeconds(tickFrequency);
        }
    }
}
    
