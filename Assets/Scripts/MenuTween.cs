using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuTween : MonoBehaviour
{
    void Start()
    {
        SlideinButtons();
    }

    public RectTransform[] menuButtons;
    public float tweenDuration = 2f;
    public float delayBetweenButtons = 0.2f;

    void SlideinButtons()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            RectTransform button = menuButtons[i];
            Vector2 originalPosition = button.anchoredPosition;
            button.anchoredPosition = new Vector2(-500f, originalPosition.y);
            button.DOAnchorPos(originalPosition, tweenDuration).SetDelay(i * delayBetweenButtons).SetEase(Ease.OutBack);
        }
    }

}

