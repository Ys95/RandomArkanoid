using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI display;

    float _fps;
    float _averageFps;
    float _displayedFps;
    int _counter;

    void UpdateDisplay()
    {
        display.text = "FPS: " + _displayedFps;
    }

    void Update()
    {
        _fps = 1 / Time.unscaledDeltaTime;
        _averageFps += _fps;
        _counter++;

        if (_counter >= 10)
        {
            _averageFps /= _counter;
            _displayedFps = Mathf.Round(_averageFps);
            UpdateDisplay();

            _averageFps = 0f;
            _counter = 0;
        }
    }
}