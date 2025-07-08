using TMPro;
using UnityEngine;

public class TextInstantShowAnimation : ITextShowAnimationStrategy
{
    public void PlayShowAnimation(TextMeshProUGUI label, string text, MonoBehaviour context, System.Action onComplete = null)
    {
        label.text = text;
        label.alpha = 1;
        onComplete?.Invoke();
    }

    public void Skip()
    {
        // Instant のため、何もする必要はありません。
    }
}