using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColorInvertEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button btn;
    
    [Space]
    [SerializeField] ButtonPart buttonPart1;
    [SerializeField] ButtonPart buttonPart2;

    void Awake()
    {
        buttonPart1.SetDefaultColors();
        buttonPart2.SetDefaultColors();
    }

    void OnDisable()
    {
        buttonPart1.RestoreDefaultColors();
        buttonPart2.RestoreDefaultColors();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Invert();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Invert();
    }

    void Invert()
    {
        if (btn.interactable == false) return;

        var tempBgColor = buttonPart1.Background.color;
        var tempTextColor = buttonPart1.Text.color;

        buttonPart1.Background.color = buttonPart2.Background.color;
        buttonPart1.Text.color = buttonPart2.Text.color;

        buttonPart2.Background.color = tempBgColor;
        buttonPart2.Text.color = tempTextColor;
    }

    [Serializable]
    struct ButtonPart
    {
        public Image Background;
        public TextMeshProUGUI Text;
        Color _defaultBgColor;
        Color _defaultTextColor;

        public void SetDefaultColors()
        {
            _defaultBgColor = Background.color;
            _defaultTextColor = Text.color;
        }

        public void RestoreDefaultColors()
        {
            Background.color = _defaultBgColor;
            Text.color = _defaultTextColor;
        }
    }
}