using UnityEngine;
using UnityEngine.UI;

public class UIImageView : MonoBehaviour
{
    enum ImageShowAnimationType
    {
        FadeIn,
        Animator,
        Instant
    }
    enum ImageHideAnimationType
    {
        FadeOut,
        Instant
    }

    [SerializeField]
    private Image image;
    [SerializeField]
    private UIImageInitializer imageInitializer;

    [SerializeField]
    private ImageShowAnimationType showAnimationType = ImageShowAnimationType.Instant;
    [SerializeField]
    private float fadeInDuration = 1f;
    [SerializeField]
    private ImageHideAnimationType hideAnimationType = ImageHideAnimationType.Instant;
    [SerializeField]
    private float fadeOutDuration = 1f;
    [SerializeField]
    private Animator animator;

    private IImageShowAnimationStrategy showAnimationStrategy;
    private IImageHideAnimationStrategy hideAnimationStrategy;

    private Sprite pendingSprite;

    private bool isVisible;

    // �e�X�g�p
    /*
    void Start()
    {
        HideImmediate();
        //ChangeImage("Ch1_2");
            ShowWithCallback (() =>
        {
            Debug.Log("�I�����");
        });
        StartCoroutine(SkipAfterDelay(3f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowImmediate();
        //ChangeImage("Ch1_3");
    }
    */
    //�e�X�g�p

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        isVisible = true;
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
        if (pendingSprite != null)
        {
            image.sprite = pendingSprite;
            pendingSprite = null;
        }

        if (showAnimationStrategy != null && image != null)
        {
            showAnimationStrategy.PlayShowAnimation(image, this, () =>
            {
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
        isVisible = true;
    }

    public void ShowImmediate()
    {
        if (pendingSprite != null)
        {
            image.sprite = pendingSprite;
            pendingSprite = null;
        }

        if (showAnimationStrategy != null)
        {
            showAnimationStrategy.Skip(); // �\���A�j���[�V�������X�L�b�v�i��Ԃ����ς���j
        }

        // �ǂ�Ȏ����F�����̓��Z�b�g����
        if (image != null && imageInitializer != null)
        {
            imageInitializer.ResetColor();
        }
        isVisible = true;
    }

    public void Hide()
    {
        HideWithCallback(null);
    }

    public void HideWithCallback(System.Action onComplete)
    {
        if (hideAnimationStrategy != null && image != null)
        {
            hideAnimationStrategy.PlayHideAnimation(image, this, () =>
            {
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
        isVisible = false;
    }

    public void HideImmediate()
    {
        if (hideAnimationStrategy != null)
        {
            hideAnimationStrategy.Skip(); // ��\���A�j���[�V�������X�L�b�v�i��Ԃ����ς���j
        }
        if (image != null)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
        isVisible = false;
    }

    public void SetSprite(Sprite sprite)
    {
        // �����f�����A����Show���ɓK�p�����悤�ۗ�
        pendingSprite = sprite;
    }

    public void ClearSprite()
    {
        image.sprite = null;
    }

    public void SetColor(ColorType colorType)
    {
        imageInitializer.SetColor(colorType);
    }

    public void ResetColor()
    {
        imageInitializer.ResetColor();
    }

    public bool IsVisible()
    {
        return isVisible;
    }


    private IImageShowAnimationStrategy CreateShowStrategy(ImageShowAnimationType type)
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        switch (type)
        {
            case ImageShowAnimationType.FadeIn:
                return new ImageFadeInAnimation(fadeInDuration);
            case ImageShowAnimationType.Animator:
                if (animator != null)
                {
                    animator.enabled = true;
                }
                return GetComponent<ImageAnimatorAnimation>();
            case ImageShowAnimationType.Instant:
            default:
                return new ImageInstantShowAnimation();
        }
    }
    private IImageHideAnimationStrategy CreateHideStrategy(ImageHideAnimationType type)
    {
        switch (type)
        {
            case ImageHideAnimationType.FadeOut: return new ImageFadeOutAnimation(fadeOutDuration);
            case ImageHideAnimationType.Instant:
            default: return new ImageInstantHideAnimation();
        }
    }
}
