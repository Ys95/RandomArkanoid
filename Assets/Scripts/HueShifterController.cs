using System;
using System.Collections;
using UnityEngine;

public class HueShifterController : MonoBehaviour
{
    [SerializeField] HSVColor hsvColor;
    [Range(0f,1f)][SerializeField] float tickFrequency;
    [Range(0f,1f)][SerializeField] float amountPerTick;

    public Action<Color> HueShift;
    
    [System.Serializable]
    struct HSVColor
    {
        [Range(0f,1f)] public float h;
        [Range(0f,1f)] public float s;
        [Range(0f,1f)] public float v;

        public HSVColor(float h, float s, float v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }
    }

    void Awake()
    {
        hsvColor.h = UnityEngine.Random.Range(0f, 1f);
        StartCoroutine(HueShiftCoroutine());
    }

    Color GetColor()
    {
        hsvColor.h += amountPerTick;
        if (hsvColor.h > 1f) hsvColor.h = 0f;
        
        return Color.HSVToRGB(hsvColor.h, hsvColor.s, hsvColor.v);
    }

    IEnumerator HueShiftCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            HueShift?.Invoke(GetColor());
            yield return new WaitForSeconds(tickFrequency);
        }
    }
}
