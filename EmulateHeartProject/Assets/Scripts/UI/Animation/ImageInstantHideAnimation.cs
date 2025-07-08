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
        // Instant �̂��߁A�X�L�b�v�����͕s�v�ł��B�����ɏI�����܂��B
    }
}
