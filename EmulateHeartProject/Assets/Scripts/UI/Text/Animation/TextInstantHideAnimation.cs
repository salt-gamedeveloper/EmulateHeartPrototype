using TMPro;
using UnityEngine;

public class TextInstantHideAnimation : ITextHideAnimationStrategy
{
    public void PlayHideAnimation(TextMeshProUGUI label, MonoBehaviour context, System.Action onComplete = null)
    {
        label.alpha = 0f;
        onComplete?.Invoke();
    }
    public void Skip()
    {
        // Instant �̂��߁A�X�L�b�v�����͕s�v�ł��B�����ɏI�����܂��B
    }
}