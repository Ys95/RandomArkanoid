using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHueShifter : HueShifter
{
    [SerializeField] Image image;
    
    protected override void HueShift(Color color)
    {
        image.color = color;
    }
}
