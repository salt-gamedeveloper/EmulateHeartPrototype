using TMPro;
using UnityEngine;

public class UITextView : MonoBehaviour
{
    private enum TextShowAnimationType
    {
        Typewriter,
        FadeIn,
        Instant
    }

    private enum TextHideAnimationType
    {
        FadeOut,
        Instant
    }

    [SerializeField]
    private TextMeshProUGUI textLabel;
    [SerializeField]
    private UITextInitializer textInitializer;

    [SerializeField]
    private TextShowAnimationType showAnimationType = TextShowAnimationType.Instant;
    [SerializeField]
    private float fadeInDuration = 1f;
    [SerializeField]
    private float TypewriterInterval = 0.05f;
    [SerializeField]
    private TextHideAnimationType hideAnimationType = TextHideAnimationType.Instant;
    [SerializeField]
    private float fadeOutDuration = 1f;

    private ITextShowAnimationStrategy showAnimationStrategy;
    private ITextHideAnimationStrategy hideAnimationStrategy;
    private string text;

    // �e�X�g�p
    /*
    void Start()
    {
        text = textLabel.text;
        ShowWithCallback (() =>
        {
            Debug.Log("�I�����");
        });
        StartCoroutine(SkipAfterDelay(1f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowImmediate();
    }
    */
    //�e�X�g�p

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        ApplySettings();
    }
    private void ApplySettings()
    {
        showAnimationStrategy = CreateShowStrategy(showAnimationType);
        hideAnimationStrategy = CreateHideStrategy(hideAnimationType);
    }

    public void Show()
    {
        ShowWithCallback(null);
    }
    public void ShowWithCallback(System.Action onComplete)
    {
        if (showAnimationStrategy != null && textLabel != null)
        {
            showAnimationStrategy.PlayShowAnimation(textLabel, text, this, () =>
            {
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
    }
    public void ShowImmediate()
    {
        if (showAnimationStrategy != null)
        {
            showAnimationStrategy.Skip(); // �\���A�j���[�V�������X�L�b�v�i��Ԃ����ς���j
        }
        else if (textLabel != null)
        {
            textLabel.alpha = 1;
        }

        textLabel.text = text;
    }

    public void Hide()
    {
        HideWithCallback(null);
    }

    public void HideWithCallback(System.Action onComplete)
    {
        if (hideAnimationStrategy != null && textLabel != null)
        {
            hideAnimationStrategy.PlayHideAnimation(textLabel, this, () =>
            {
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
    }

    public void HideImmediate()
    {
        if (hideAnimationStrategy != null)
        {
            hideAnimationStrategy.Skip(); // ��\���A�j���[�V�������X�L�b�v�i��Ԃ����ς���j
        }
        else if (textLabel != null)
        {
            textLabel.alpha = 0;
        }
    }

    /// <summary>
    /// �e�L�X�g�̐F�𒼐ڕύX���܂��B
    /// </summary>
    /// <param name="color">�ݒ肷��J���[</param>
    public void SetTextColor(ColorType color)
    {
        textInitializer.SetColor(color);
    }

    /// <summary>
    /// �����ݒ�̃e�L�X�g�J���[�ɖ߂��܂��B
    /// </summary>
    public void ResetColor()
    {
        textInitializer.ResetColor();
    }

    /// <summary>
    /// �e�L�X�g�̓��e��ݒ肵�܂��B
    /// �e�L�X�g��ݒ肵�Ă��\������܂���B
    /// </summary>
    /// <param name="text">�\�����镶����</param>
    public void SetText(string text)
    {
        this.text = text;
    }

    /// <summary>
    /// �e�L�X�g�̃X�s�[�h��ݒ肵�܂��B
    /// </summary>
    /// <param name="interval">�e�L�X�g�̑��x</param>
    public void SetTextSpeed(float interval)
    {
        if (interval < 0.01f)
        {
            interval = 0.01f;
        }
        TypewriterInterval = interval;
    }

    private ITextShowAnimationStrategy CreateShowStrategy(TextShowAnimationType type)
    {
        switch (type)
        {
            case TextShowAnimationType.FadeIn: return new TextFadeInAnimation(fadeInDuration);
            case TextShowAnimationType.Instant: return new TextInstantShowAnimation();
            case TextShowAnimationType.Typewriter:
            default: return new TextTypewriterAnimation(TypewriterInterval);
        }
    }
    private ITextHideAnimationStrategy CreateHideStrategy(TextHideAnimationType type)
    {
        switch (type)
        {
            case TextHideAnimationType.FadeOut: return new TextFadeOutAnimation(fadeOutDuration);
            case TextHideAnimationType.Instant:
            default: return new TextInstantHideAnimation();
        }
    }
}
