using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TIPPopUpTween : MonoBehaviour
{
    public RectTransform titleImage;
    public float tweenDuration = 1f;
    public float displayDuration = 2f;
    public float startOffsetX = 500f;
    public float endOffsetX = 500f;
    public Ease slideInEase = Ease.OutBack;
    public Ease slideOutEase = Ease.InBack;

    private Vector2 originalPosition;
    private Sequence titleSequence;

    void Start()
    {
        StartTitleAnimation();
    }

    void StartTitleAnimation()
    {
        if (titleImage == null) return;

        originalPosition = titleImage.anchoredPosition;

        titleSequence = DOTween.Sequence();

        titleImage.anchoredPosition = new Vector2(startOffsetX, originalPosition.y);
        titleImage.gameObject.SetActive(true);

        titleSequence.Append(
            titleImage.DOAnchorPos(originalPosition, tweenDuration)
                .SetEase(slideInEase)
        );

        titleSequence.AppendInterval(displayDuration);

        titleSequence.Append(
            titleImage.DOAnchorPos(new Vector2(endOffsetX, originalPosition.y), tweenDuration)
                .SetEase(slideOutEase)
        );

        titleSequence.OnComplete(() => {
            titleImage.gameObject.SetActive(false);
            titleImage.anchoredPosition = originalPosition;
        });

        titleSequence.Play();
    }

    void OnDestroy()
    {
        if (titleSequence != null && titleSequence.IsActive())
        {
            titleSequence.Kill();
        }
    }
}