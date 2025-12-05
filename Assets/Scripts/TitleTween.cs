using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleTween : MonoBehaviour
{
    public RectTransform titleImage;
    public float tweenDuration = 2f;
    public float startOffsetX = -500f;
    public Ease tweenEase = Ease.OutBack;

    void Start()
    {
        SlideInTitle();
    }

    void SlideInTitle()
    {
        if (titleImage == null) return;

        Vector2 originalPosition = titleImage.anchoredPosition;
        titleImage.anchoredPosition = new Vector2(startOffsetX, originalPosition.y);
        titleImage.DOAnchorPos(originalPosition, tweenDuration).SetEase(tweenEase);
    }
}
