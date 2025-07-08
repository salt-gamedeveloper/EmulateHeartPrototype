using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeOutAnimation : SkippableAnimationBase, IImageHideAnimationStrategy
{
    private readonly float duration;
    private bool isPlaying;
    private Image image;

    public ImageFadeOutAnimation(float duration)
    {
        isPlaying = false;
        if (duration <= 0f)
            throw new System.ArgumentOutOfRangeException(nameof(duration), "duration must be greater than 0.");

        this.duration = duration;
    }

    public void PlayHideAnimation(Image image, MonoBehaviour context, System.Action onComplete = null)
    {

        isPlaying = true;
        this.image = image;
        Play(context, onComplete);
    }

    protected override IEnumerator PlayRoutine()
    {
        float time = 0f;
        Color color = image.color;
        float startAlpha = color.a;

        while (time < duration && isPlaying)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            color.a = alpha;
            image.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        image.color = color;

        Complete();
    }

    protected override void OnSkip()
    {
        isPlaying = false;
        Color color = image.color;
        color.a = 0f;
        image.color = color;
    }
}
