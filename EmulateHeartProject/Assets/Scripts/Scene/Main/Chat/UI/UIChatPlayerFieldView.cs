using System.Collections.Generic;
using UnityEngine;

public class UIChatPlayerFieldView : UIFieldViewBase
{
    [SerializeField]
    private UIChatInputField inputField;
    [SerializeField]
    private UIChatOptionsField optionsField;
    [SerializeField]
    private UIButtonView newOptionsButtonView;
    [SerializeField]
    private UIButtonView chatLogButtonView;
    [SerializeField]
    private UIButtonView timeAdvanceButtonView;

    private System.Action<string> OnChatSendAction;

    private System.Action OnChatLogClicked;
    private System.Action OnTimeAdvanceClicked;
    private System.Action OnNewOptionsClicked;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        inputField.SubscribeToChatSend(HandleChatSend);
        optionsField.SubscribeToOptionSelected(HandleChatSend);

    }

    public void SceneReset()
    {
        HideImmediate();
        optionsField.SceneReset();
        StartResponseWait();
    }

    public void StartResponseWait()
    {
        inputField.ChatInputReset();
        optionsField.StartResponseWait();
        newOptionsButtonView.SetButtonEnabled(false);
        chatLogButtonView.SetButtonEnabled(false);
        timeAdvanceButtonView.SetButtonEnabled(false);
        canvasGroup.interactable = false;
    }

    public void StartInputWait()
    {
        canvasGroup.interactable = true;
        optionsField.StartInputWait();
        inputField.StartInputWait();
        newOptionsButtonView.SetButtonEnabled(true);
        chatLogButtonView.SetButtonEnabled(true);
        timeAdvanceButtonView.SetButtonEnabled(true);
    }

    public void ChatResponse(List<PlayerOption> playerOptions)
    {
        StartInputWait();
        optionsField.SetOptions(playerOptions);
    }

    private void HandleChatSend(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            inputField.ChatInputError();
            return;
        }

        inputField.SetChatText(message); // “ü—Í—“‚É”½‰f


        StartResponseWait();
        OnChatSendAction?.Invoke(message);
    }
    public void SubscribeToChatSend(System.Action<string> listener)
    {
        OnChatSendAction = listener;
    }
    public void SubscribeToChatLog(System.Action listener)
    {
        OnChatLogClicked += listener;
        chatLogButtonView.SubscribeToButtonClick(OnChatLogClicked);
    }
    public void SubscribeToTimeAdvance(System.Action listener)
    {
        OnTimeAdvanceClicked += listener;
        timeAdvanceButtonView.SubscribeToButtonClick(OnTimeAdvanceClicked);
    }
    public void SubscribeToNewOptions(System.Action listener)
    {
        OnNewOptionsClicked += listener;
        newOptionsButtonView.SubscribeToButtonClick(OnNewOptionsClicked);
    }
}
