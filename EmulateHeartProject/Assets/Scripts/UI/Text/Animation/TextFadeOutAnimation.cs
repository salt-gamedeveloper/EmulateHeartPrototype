using System.Collections;
using TMPro;
using UnityEngine;


public class TextFadeOutAnimation : SkippableAnimationBase, ITextHideAnimationStrategy
{
    private readonly float duration;
    private bool isPlaying;
    private TextMeshProUGUI label;

    public TextFadeOutAnimation(float duration)
    {
        isPlaying = false;
        if (duration <= 0f)
            throw new System.ArgumentOutOfRangeException(nameof(duration), "duration must be greater than 0.");

        this.duration = duration;
    }

    public void PlayHideAnimation(TextMeshProUGUI label, MonoBehaviour context, System.Action onComplete = null)
    {
        isPlaying = true;
        this.label = label;
        Play(context, onComplete);
    }

    protected override IEnumerator PlayRoutine()
    {
        float time = 0f;
        float startAlpha = label.alpha;

        while (time < duration && isPlaying)
        {
            label.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        label.alpha = 0f;
        Complete();
    }

    protected override void OnSkip()
    {
        isPlaying = false;
        label.alpha = 0f;
    }
}