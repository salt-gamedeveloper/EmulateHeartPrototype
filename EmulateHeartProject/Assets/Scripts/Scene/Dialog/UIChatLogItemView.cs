using UnityEngine;

public class UIChatLogItemView : MonoBehaviour
{
    [SerializeField]
    private UIImageView bgImageView;
    [SerializeField]
    private UITextView chatTextView;
    [SerializeField]
    private UIImageView iconImageView;
    [SerializeField]
    private CanvasGroup iconCanvasGroup;

    private RectTransform rectTransform;

    private int rowHeight;
    private int baseHeight;
    private int playerWidth;


    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        rowHeight = 45;
        baseHeight = 25;
        playerWidth = 900;
        rectTransform = GetComponent<RectTransform>();
        iconImageView.HideImmediate();
    }

    //TODO: キャラクターごとのアイコンの設定
    public void SetChatLog(ChatLogItemViewModel viewModel)
    {
        Vector2 size = rectTransform.sizeDelta;
        size.y = (viewModel.MessageRowCount * rowHeight) + baseHeight;

        MessageType messageType = viewModel.MessageType;

        switch (messageType)
        {
            case MessageType.Ai:
                iconImageView.SetColor(ColorType.AiMainhighA80);
                iconImageView.ShowImmediate();
                bgImageView.SetColor(ColorType.AiMainhighA80);
                break;
            case MessageType.Player:
                size.x = playerWidth;
                iconImageView.HideImmediate();
                bgImageView.SetColor(ColorType.PlayerMainhighA80);
                break;
            default:
                iconImageView.HideImmediate();
                break;
        }

        rectTransform.sizeDelta = size;

        chatTextView.SetText(viewModel.Message);
        chatTextView.Show();
    }
}
