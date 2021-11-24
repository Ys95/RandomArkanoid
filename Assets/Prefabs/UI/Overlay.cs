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

    GameObject _currentOverlay;

    public void TurnOn()
    {
        overlay.SetActive(true);
        DisplayIdleOverlay();
     }

    public void TurnOff()
    {
        overlay.SetActive(false);
        _currentOverlay = null;
    }

    void Display(GameObject overlay)
    {
        if (overlay == null) return;
        if (_currentOverlay != null) _currentOverlay.SetActive(false);

        overlay.SetActive(true);
        _currentOverlay = overlay;
    }

    public void DisplayIdleOverlay() => Display( idleOverlayDisplay);

    public void DisplayFailedOverlay() => Display( onFailedOverlayDisplay);

    public void DisplaySucceededOverlay() => Display( onSucceededOverlayDisplay);
}
