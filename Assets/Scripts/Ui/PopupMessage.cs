using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class PopupMessage : MonoBehaviour
    {
        [SerializeField] GameObject messageWindow;

        [Space]
        [SerializeField] TextMeshProUGUI message;

        public void Show(string msg)
        {
            messageWindow.SetActive(true);
            message.text = msg;
        }

        public void Hide()
        {
            messageWindow.SetActive(false);
        }
    }
}