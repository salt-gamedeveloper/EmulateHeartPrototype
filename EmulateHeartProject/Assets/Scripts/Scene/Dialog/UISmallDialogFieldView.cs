using DG.Tweening;
using UnityEngine;

public class UISmallDialogFieldView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Transform buttonParent;
    [SerializeField]
    private UITextView textView;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Hide();
    }

    public void InjectViewModel(SmallDialogViewModel viewModel)
    {
        ClearButtons();
        switch (viewModel.Type)
        {
            case DialogType.Info:
                CreateButton(viewModel.CloseButtonText, viewModel.OnCloseAction);
                break;
            case DialogType.YesNo:
                CreateButton(viewModel.YesButtonText, viewModel.OnYesAction);
                CreateButton(viewModel.NoButtonText, viewModel.OnNoAction);
                break;
            default:
                Debug.Log("ïsê≥Ç»DialogTypeÇ≈Ç∑");
                return;
        }
        textView.SetText(viewModel.DialogText);
    }

    public void Show()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.DOFade(1f, 0.3f);
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 newSize = new Vector2(650f, 370f);
        rect.DOSizeDelta(newSize, 0.3f)
            .OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = true;
                textView.Show();
            });

    }

    public void Hide()
    {
        HideWithCallback(null);
    }

    public void HideWithCallback(System.Action onComplete)
    {
        textView.SetText("");
        textView.Show();
        ClearButtons();
        canvasGroup.blocksRaycasts = false;

        RectTransform rect = GetComponent<RectTransform>();
        Vector2 newSize = new Vector2(585f, 333f);

        canvasGroup.DOFade(0f, 0.3f);
        rect.DOSizeDelta(newSize, 0.3f)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    private void CreateButton(string text, System.Action listener)
    {
        GameObject instance = Instantiate(buttonPrefab, buttonParent);
        UIButtonView buttonView = instance.GetComponent<UIButtonView>();
        buttonView.SetText(text);
        buttonView.SubscribeToButtonClick(listener);
    }

    private void ClearButtons()
    {
        foreach (Transform child in buttonParent)
        {
            if (child.CompareTag("DialogItem"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
