using System;
using UnityEngine;

public abstract class HueShifter : MonoBehaviour
{
    [Header("Hue shift controller")]
    [SerializeField] HueShifterController controller;

    protected HueShifterController Controller => controller;
    
    void OnEnable()
    {
        controller.ColorShift += HueShift;
    }
    
    protected abstract void HueShift(Color color);

    void OnDisable()
    {
        controller.ColorShift -= HueShift;
    }

    void OnDestroy()
    {
        controller.ColorShift -= HueShift;
    }
}
