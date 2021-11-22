using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HSVColor
{
    [Range(0f, 1f)] [SerializeField] float h;
    [Range(0f, 1f)] [SerializeField] float s;
    [Range(0f, 1f)] [SerializeField] float v;

    public float H
    {
        get => h;
        set => h = value;
    }

    public float S
    {
        get => s;
        set => s = value;
    }
        
    public float V
    {
        get => v;
        set => v = value;
    }

    public Color GetColor => Color.HSVToRGB(h, s, v);
         
    public HSVColor(float h, float s, float v)
    {
        this.h = h;
        this.s = s;
        this.v = v;
    }
}
