using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Overlay
{
    [SerializeField] GameObject overlay;
    
    [Space]
    [SerializeField] GameObject idleOverlayDisplay;
    [SerializeField] GameObject onFailedOverlayDisplay;
    [SerializeField] GameObject onSucceededOverlayDisplay;

    public void TurnOn()
    {
        overlay.SetActive(true);
        DisplayIdleOverlay(true);
        
        DisplayFailedOverlay(false);
        DisplaySucceededOverlay(false);
    }

    public void TurnOff()
    {
        overlay.SetActive(false);
        DisplayIdleOverlay(false);
        DisplayFailedOverlay(false);
        DisplaySucceededOverlay(false);
    }

    void Display(bool display, GameObject overlay)
    {
        if(overlay==null) return;

        if (display)
        {
            overlay.SetActive(true);
            return;
        }
        overlay.SetActive(false);
    }

    public void DisplayIdleOverlay(bool display) => Display(display, idleOverlayDisplay);

    public void DisplayFailedOverlay(bool display) => Display(display, onFailedOverlayDisplay);

    public void DisplaySucceededOverlay(bool display) => Display(display, onSucceededOverlayDisplay);
}
