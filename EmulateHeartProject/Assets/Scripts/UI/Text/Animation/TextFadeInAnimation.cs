using System.Collections;
using TMPro;
using UnityEngine;

public class TextFadeInAnimation : SkippableAnimationBase, ITextShowAnimationStrategy
{
    private readonly float duration;
    private string fullText;
    private bool isPlaying;
    private TextMeshProUGUI label;
    private float targetAlpha = 1f;
    public TextFadeInAnimation(float duration)
    {
        isPlaying = false;
        if (duration <= 0f)
            throw new System.ArgumentOutOfRangeException(nameof(duration), "duration must be greater than 0.");

        this.duration = duration;
    }
    public void PlayShowAnimation(TextMeshProUGUI label, string text, MonoBehaviour context, System.Action onComplete = null)
    {
        fullText = text;
        isPlaying = true;
        this.label = label;

        // 初期化クラスから alpha を取得（なければ1.0f）
        var initializer = label.GetComponent<UITextInitializer>();
        targetAlpha = initializer != null ? initializer.InitialColor.a : 1f;

        Play(context, onComplete);
    }

    protected override IEnumerator PlayRoutine()
    {
        label.text = fullText;
        label.alpha = 0f;
        float time = 0;

        while (time < duration && isPlaying)
        {
            label.alpha = Mathf.Lerp(0f, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        label.alpha = targetAlpha;
        Complete();
    }

    protected override void OnSkip()
    {
        isPlaying = false;
        label.alpha = targetAlpha;
    }
}