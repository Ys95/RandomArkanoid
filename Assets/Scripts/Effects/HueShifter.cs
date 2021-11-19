using System;
using UnityEngine;

public abstract class HueShifter : MonoBehaviour
{
    [Header("Hue shift controller")]
    [SerializeField] HueShifterController controller;

    void OnValidate()
    {
        controller.HueShift += HueShift;
    }

    void OnEnable()
    {
        controller.HueShift += HueShift;
    }
    
    protected abstract void HueShift(Color color);

    void OnDisable()
    {
        controller.HueShift -= HueShift;
    }
}
