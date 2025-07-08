using UnityEngine;
using UnityEngine.UI;

public class ImageInstantHideAnimation : IImageHideAnimationStrategy
{

    public void PlayHideAnimation(Image image, MonoBehaviour context, System.Action onComplete = null)
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        onComplete?.Invoke();
    }
    public void Skip()
    {
        // Instant のため、スキップ処理は不要です。即座に終了します。
    }
}
