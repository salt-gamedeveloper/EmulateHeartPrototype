using UnityEngine;

public class UIChatInputField : MonoBehaviour
{
    [SerializeField]
    private UIInputFieldView chatInputFieldView;
    [SerializeField]
    private UIButtonView sendButtonView;
    [SerializeField]
    private UITextView countTextView;

    private int characterLimit = 80;
    private string chatText = "";

    System.Action<string> OnChatSendAction;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        chatInputFieldView.SubscribeToInputTextCount(UpdateChatInput);
        chatInputFieldView.SubscribeToShiftEnter(OnChatSend);
        chatInputFieldView.SetCharacterLimit(characterLimit);
        sendButtonView.SubscribeToButtonClick(OnChatSend);

        SceneReset();
    }
    public void SceneReset()
    {
        chatText = "";
        UpdateChatInput(0);
        chatInputFieldView.SetText(chatText);
        StartInputWait();
    }

    public void StartInputWait()
    {
        chatInputFieldView.SetPlaceholderText("80文字まで入力できます");

        chatInputFieldView.SetInputEnabled(true);
        sendButtonView.SetButtonEnabled(true);
        chatText = "";
        UpdateChatInput(0);

        chatInputFieldView.FocusInput();
    }

    public void SetChatText(string chat)
    {
        chatText = chat;
        chatInputFieldView.SetText(chatText);
    }

    private void UpdateChatInput(int count)
    {
        countTextView.SetText(count.ToString("D2") + "文字");
        if (count >= characterLimit)
        {
            countTextView.SetText("これ以上入力できません");
        }
        countTextView.Show();
    }

    public void ChatInputReset()
    {
        chatInputFieldView.SetInputEnabled(false);
        sendButtonView.SetButtonEnabled(false);
        chatInputFieldView.SetPlaceholderText(chatText);
        chatInputFieldView.SetText("");
        countTextView.SetText("返信待ち...");
        countTextView.Show();
    }

    public void ChatInputError()
    {
        chatInputFieldView.SetPlaceholderText("1文字以上入力してください");
    }
    public void OnChatSend()
    {
        chatInputFieldView.OnEndEdit();
        chatText = chatInputFieldView.GetText();
        //ChatInputReset();
        OnChatSendAction?.Invoke(chatText);
    }
    public void SubscribeToChatSend(System.Action<string> listener)
    {
        OnChatSendAction = listener;
    }
}
