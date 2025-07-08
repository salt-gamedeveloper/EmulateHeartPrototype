using DG.Tweening;
using UnityEngine;

public class UISideMenuFieldView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private UIButtonView loadGameButtonView;
    [SerializeField]
    private UIButtonView statusButtonView;
    [SerializeField]
    private UIButtonView diaryButtonView;
    [SerializeField]
    private UIButtonView settingButtonView;
    [SerializeField]
    private UIButtonView titleButtonView;

    [SerializeField]
    private UIButtonView closeButtonView;


    private System.Action OnCloseClicked;

    private System.Action OnLoadGameClicked;
    private System.Action OnStatusClicked;
    private System.Action OnDiaryClicked;
    private System.Action OnSettingClicked;
    private System.Action OnTitleClicked;

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        OnLoadGameClicked = () =>
        {
            SetInteractable(false);
        };

        OnStatusClicked = () =>
        {
            SetInteractable(false);
        };

        OnDiaryClicked = () =>
        {
            SetInteractable(false);
        };

        OnSettingClicked = () =>
        {
            SetInteractable(false);
        };

        OnTitleClicked = () =>
        {
            SetInteractable(false);
        };

        Hide();
    }


    public void Show()
    {
        canvasGroup.DOFade(1f, 0.3f);
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 newSize = new Vector2(640f, rect.sizeDelta.y);
        rect.DOSizeDelta(newSize, 0.3f)
            .OnComplete(() =>
            {
                SetInteractable(true);
            });
    }

    public void Hide()
    {
        HideWithCallback(null);
    }

    public void HideWithCallback(System.Action onComplete)
    {
        SetInteractable(false);

        RectTransform rect = GetComponent<RectTransform>();
        Vector2 newSize = new Vector2(400f, rect.sizeDelta.y);

        canvasGroup.DOFade(0f, 0.3f);
        rect.DOSizeDelta(newSize, 0.3f)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void HideImmediate()
    {
        canvasGroup.alpha = 0f;
        SetInteractable(false);
    }

    private void SetInteractable(bool isInteractable)
    {
        canvasGroup.interactable = isInteractable;
        canvasGroup.blocksRaycasts = isInteractable;
        loadGameButtonView.SetButtonEnabled(isInteractable);
        statusButtonView.SetButtonEnabled(isInteractable);
        diaryButtonView.SetButtonEnabled(isInteractable);
        settingButtonView.SetButtonEnabled(isInteractable);
        titleButtonView.SetButtonEnabled(isInteractable);
    }

    public void SubscribeToLoadGame(System.Action listener)
    {
        OnLoadGameClicked += listener;
        loadGameButtonView.SubscribeToButtonClick(() => OnLoadGameClicked?.Invoke());
    }
    public void SubscribeToStatus(System.Action listener)
    {
        OnStatusClicked += listener;
        statusButtonView.SubscribeToButtonClick(() => OnStatusClicked?.Invoke());
    }
    public void SubscribeToDiary(System.Action listener)
    {
        OnDiaryClicked += listener;
        diaryButtonView.SubscribeToButtonClick(() => OnDiaryClicked?.Invoke());
    }
    public void SubscribeToSetting(System.Action listener)
    {
        OnSettingClicked += listener;
        settingButtonView.SubscribeToButtonClick(() => OnSettingClicked?.Invoke());
    }
    public void SubscribeToTitle(System.Action listener)
    {
        OnTitleClicked += listener;
        titleButtonView.SubscribeToButtonClick(() => OnTitleClicked?.Invoke());
    }

    public void SubscribeToDialogClose(System.Action listener)
    {
        OnCloseClicked += listener;
        closeButtonView.SubscribeToButtonClick(() => OnCloseClicked?.Invoke());
    }
}
