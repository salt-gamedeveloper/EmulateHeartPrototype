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
        // Instant のため、スキップ処理は不要です。即座に終了します。
    }
}