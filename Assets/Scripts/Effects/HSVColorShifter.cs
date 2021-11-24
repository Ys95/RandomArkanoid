using UnityEngine;

public abstract class HSVColorShifter : MonoBehaviour
{
    [Header("Color shift controller")]
    [SerializeField] HSVColorShifterController controller;

    protected HSVColorShifterController Controller => controller;

    void OnEnable()
    {
        controller.ColorShift += ColorShift;
    }

    void OnDisable()
    {
        controller.ColorShift -= ColorShift;
    }

    void OnDrawGizmos()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        ColorShift(controller.StartingColor.GetColor);
    }

    protected abstract void ColorShift(Color color);
}