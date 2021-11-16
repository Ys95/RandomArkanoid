using System;
using System.Collections;
using UnityEngine;

public class HueChangerController : MonoBehaviour
{
    [SerializeField] HSVColor hsvColor;
    [SerializeField] float frequency;

    IEnumerator _coroutine;
    public Action<Color> HueShift;
    
    bool _isActive;
    
    [System.Serializable]
    struct HSVColor
    {
        public float h;
        public float s;
        public float v;

        public HSVColor(float h, float s, float v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }
    }

    void OnValidate()
    {
        Color color = Color.HSVToRGB(hsvColor.h, hsvColor.s, hsvColor.v);
        HueShift?.Invoke(color);
    }

    public void Activate()
    {
        if(_isActive) return;
        
      //  StartCoroutine(ColorSwapCoroutine())
    }
    
    Color GetColor()
    {
        if (hsvColor.h < 1f) hsvColor.h += 0.01f;
        else hsvColor.h = 0f;
        Color color = Color.HSVToRGB(hsvColor.h, hsvColor.s, hsvColor.v);
        return color;
    }

    IEnumerator ColorSwapCoroutine()
    {
        _isActive = true;
        while (HueShift != null)
        {
            HueShift?.Invoke(GetColor());
            yield return new WaitForSeconds(frequency);
        }
        _isActive = false;
    }
}

