using UnityEngine;

public abstract class HueChanger : MonoBehaviour
{
    [SerializeField] HueShifterController controller;

    void Awake()
    {
        controller.HueShift += HueShift;
    }
    
    protected abstract void HueShift(Color color);

    void OnDisable()
    {
        controller.HueShift -= HueShift;
    }
}
