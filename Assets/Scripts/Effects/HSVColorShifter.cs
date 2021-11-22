using System;
using UnityEngine;

public abstract class HSVColorShifter : MonoBehaviour
{
    [Header("Color shift controller")]
    [SerializeField] HSVColorShifterController controller;

    HSVColor startingColor;
    
    protected HSVColorShifterController Controller => controller;

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        ColorShift(controller.StartingColor.GetColor);
    }
    
    void OnEnable()
    {
        controller.ColorShift += ColorShift;
    }
    
    protected abstract void ColorShift(Color color);

    void OnDisable()
    {
        controller.ColorShift -= ColorShift;
    }

    void OnDrawGizmos()
    {
        Refresh();
    }
}
