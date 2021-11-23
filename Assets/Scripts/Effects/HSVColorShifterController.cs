using System;
using System.Collections;
using UnityEngine;

public class HSVColorShifterController : MonoBehaviour
{
    [SerializeField] HSVColor startingColor;
    
    [Space]
    [SerializeField] ShiftedValue hue;
    [SerializeField] ShiftedValue value;
    [SerializeField] ShiftedValue saturation;
    
    [Space]
    [Range(0f, 1f)] public float tickFrequency;
    
    public Action<Color> ColorShift;

    public HSVColor StartingColor
    {
        get => startingColor;
        set => startingColor = value;
    }

    void OnEnable()
    {
        StartCoroutine(ColorShiftCoroutine());
    }

    Color GetColor()
    {
        if (hue.IsShifted) startingColor.H = hue.Shift(StartingColor.H);
        if (saturation.IsShifted) startingColor.S = saturation.Shift(StartingColor.S);
        if (value.IsShifted) startingColor.V = value.Shift(StartingColor.V);

        return Color.HSVToRGB(StartingColor.H, StartingColor.S, StartingColor.V);
    }

    IEnumerator ColorShiftCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            ColorShift?.Invoke(GetColor());
            yield return new WaitForSeconds(tickFrequency);
        }
    }

    [Serializable]
    struct ShiftedValue
    {
        [SerializeField] bool shift;
        [SerializeField] bool pingPongMode;
        [Space]
        [Range(0f, 1f)] [SerializeField] float amountPerTick;
        [Space]
        [Range(0f, 1f)] [SerializeField] float min;
        [Range(0f, 1f)] [SerializeField] float max;
        bool _pingPongReverse;
        public bool IsShifted => shift;

        public float Shift(float hsv)
        {
            if (pingPongMode) return PingPongShift(hsv);
            return NormalShift(hsv);
        }

        float NormalShift(float hsv)
        {
            hsv += amountPerTick;
            if (hsv >= max) hsv = min;
            return hsv;
        }

        float PingPongShift(float hsv)
        {
            if (!_pingPongReverse)
            {
                hsv += amountPerTick;
                if (hsv >= max) _pingPongReverse = true;
            }
            else
            {
                hsv -= amountPerTick;
                if (hsv <= min) _pingPongReverse = false;
            }

            return hsv;
        }
    }
}