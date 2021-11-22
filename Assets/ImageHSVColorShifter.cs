using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHSVColorShifter : HSVColorShifter
{
    [SerializeField] Image image;
    
    protected override void ColorShift(Color color)
    {
        image.color = color;
    }
}
