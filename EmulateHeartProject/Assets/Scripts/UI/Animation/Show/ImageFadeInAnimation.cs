using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInAnimation : SkippableAnimationBase, IImageShowAnimationStrategy
{
    private readonly float duration;
    private bool isPlaying;
    private Image image;
    private float targetAlpha = 1f;

    public ImageFadeInAnimation(float duration)
    {
        isPlaying = false;
        if (duration <= 0f)
            throw new System.ArgumentOutOfRangeException(nameof(duration), "duration must be greater than 0.");

        this.duration = duration;
    }

    public void PlayShowAnimation(Image image, MonoBehaviour context, System.Action onComplete = null)
    {

        isPlaying = true;
        this.image = image;

        var initializer = image.GetComponent<UIImageInitializer>();
        targetAlpha = initializer != null ? initializer.InitialColor.a : 1f;

        Play(context, onComplete);
    }

    protected override IEnumerator PlayRoutine()
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        float time = 0;

        while (time < duration && isPlaying)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, time / duration);
            color.a = alpha;
            image.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = targetAlpha;
        image.color = color;

        Complete();
    }

    protected override void OnSkip()
    {
        isPlaying = false;
        Color color = image.color;
        color.a = targetAlpha;
        image.color = color;
    }
}
