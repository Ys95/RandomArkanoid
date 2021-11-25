using TMPro;
using UnityEngine;

namespace UI
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI display;
        float _averageFps;
        int _counter;
        float _displayedFps;

        float _fps;

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

        void UpdateDisplay()
        {
            display.text = "FPS: " + _displayedFps;
        }
    }
}