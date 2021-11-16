using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColorInvertEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    struct ButtonPart
    {
        public Image Background;
        public TextMeshProUGUI Text;
    }

    [SerializeField] ButtonPart buttonPart1;
    [SerializeField] ButtonPart buttonPart2;
    
    void Invert()
    {
        Color tempBgColor = buttonPart1.Background.color;
        Color tempTextColor = buttonPart1.Text.color;

        buttonPart1.Background.color = buttonPart2.Background.color;
        buttonPart1.Text.color = buttonPart2.Text.color;

        buttonPart2.Background.color = tempBgColor;
        buttonPart2.Text.color = tempTextColor;
    }
    
    public void OnPointerEnter(PointerEventData eventData) => Invert();

    public void OnPointerExit(PointerEventData eventData) => Invert();
}

