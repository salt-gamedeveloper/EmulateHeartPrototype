using System.Collections;
using TMPro;
using UnityEngine;

public class TextTypewriterAnimation : SkippableAnimationBase, ITextShowAnimationStrategy
{
    private float characterInterval;
    private string fullText;
    private TextMeshProUGUI label;


    public TextTypewriterAnimation(float interval)
    {
        characterInterval = Mathf.Max(0f, interval);
    }

    public void PlayShowAnimation(TextMeshProUGUI label, string text, MonoBehaviour context, System.Action onComplete = null)
    {
        this.label = label;
        fullText = text;
        label.text = "";

        Play(context, onComplete);
    }

    protected override IEnumerator PlayRoutine()
    {
        label.text = "";
        label.alpha = 1;
        var sb = new System.Text.StringBuilder();
        foreach (char c in fullText)
        {
            sb.Append(c);
            label.text = sb.ToString();
            yield return new WaitForSecondsRealtime(characterInterval);
        }

        label.text = fullText;
        Complete();
    }

    protected override void OnSkip()
    {
        label.text = fullText;
    }
}