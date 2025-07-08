using DG.Tweening;
using UnityEngine;

//TODO: �A�j���[�V�����̊Ǘ���ʂōs��
public class UIFieldViewBase : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void Show()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.7f)
            .OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = true;
            });
    }

    public void Hide()
    {
        canvasGroup.DOFade(0f, 0.3f)
            .OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = false;
            });
    }
    public void HideImmediate()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}