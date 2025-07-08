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
        chatInputFieldView.SetPlaceholderText("80�����܂œ��͂ł��܂�");

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
        countTextView.SetText(count.ToString("D2") + "����");
        if (count >= characterLimit)
        {
            countTextView.SetText("����ȏ���͂ł��܂���");
        }
        countTextView.Show();
    }

    public void ChatInputReset()
    {
        chatInputFieldView.SetInputEnabled(false);
        sendButtonView.SetButtonEnabled(false);
        chatInputFieldView.SetPlaceholderText(chatText);
        chatInputFieldView.SetText("");
        countTextView.SetText("�ԐM�҂�...");
        countTextView.Show();
    }

    public void ChatInputError()
    {
        chatInputFieldView.SetPlaceholderText("1�����ȏ���͂��Ă�������");
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
