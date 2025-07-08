using UnityEngine;
using UnityEngine.UI;

public class ImageInstantShowAnimation : IImageShowAnimationStrategy
{
    private UIImageInitializer initializer;

    public void PlayShowAnimation(Image image, MonoBehaviour context, System.Action onComplete = null)
    {
        if (initializer == null)
            initializer = image.GetComponent<UIImageInitializer>();

        if (initializer != null)
        {
            initializer.ResetColor();
        }
        else
        {
            var color = image.color;
            color.a = 1f;
            image.color = color;
        }

        onComplete?.Invoke();
    }
    public void Skip()
    {
        // Instant のため、スキップ処理は不要です。即座に終了します。
    }
}
