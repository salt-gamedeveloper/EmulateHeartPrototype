using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIChatLogFieldView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private UIButtonView closeButtonView;

    [SerializeField]
    private GameObject chatLogItemPrefab;
    [SerializeField]
    private Transform chatLogItemParent;
    [SerializeField]
    private ScrollRect scrollRect;

    private System.Action OnCloseClicked;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        ClearChatLogItems();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

    }

    public void InjectViewModel(ChatLogViewModel viewModel)
    {
        string title = viewModel.Titile;

        foreach (var item in viewModel.ChatLogItems)
        {
            CreateChatLogItem(item);
        }
        ScrollToBottom();
    }

    public void Show()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.DOFade(1f, 0.3f);
        canvasGroup.blocksRaycasts = true;

    }

    public void HideWithCallback(System.Action onComplete)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0f, 0.3f)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                ClearChatLogItems();
            });
    }

    private void CreateChatLogItem(ChatLogItemViewModel viewModel)
    {
        GameObject instance = Instantiate(chatLogItemPrefab, chatLogItemParent);
        UIChatLogItemView chatLogItemView = instance.GetComponent<UIChatLogItemView>();
        chatLogItemView.SetChatLog(viewModel);
    }

    private void ScrollToBottom()
    {
        // ��ԉ��ɃX�N���[���i0�����A1����j
        Canvas.ForceUpdateCanvases(); // ���C�A�E�g�X�V��ۏ�
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private void ClearChatLogItems()
    {
        foreach (Transform child in chatLogItemParent)
        {
            if (child.CompareTag("ChatLogItem"))
            {
                Destroy(child.gameObject);
            }
        }
    }


    public void SubscribeToDialogClose(System.Action listener)
    {
        OnCloseClicked += listener;
        closeButtonView.SubscribeToButtonClick(() => OnCloseClicked?.Invoke());
    }

}
