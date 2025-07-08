using UnityEngine;

public class ChatSceneUIController : UIControllerBase
{
    [SerializeField]
    private UIBGImageView bgImageView;

    [SerializeField]
    private UICharacterImageView aiCharacterView;
    [SerializeField]
    private UIChatPlayerFieldView playerFieldView;
    [SerializeField]
    private UIChatConversationFieldView conversationFieldView;
    [SerializeField]
    private UIChatInformationFieldView informationFieldView;


    private System.Action OnChatLogClicked;
    private System.Action OnTimeAdvanceClicked;
    private System.Action OnNewOptionsClicked;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Close();
    }

    public void SceneReset()
    {
        bgImageView.SceneReset();
        playerFieldView.SceneReset();
        aiCharacterView.SceneReset();
        conversationFieldView.SceneReset();
        informationFieldView.SceneReset();
    }
    public void Open()
    {
        Show();
    }

    public void Close()
    {
        Hide();
    }

    public void Reopen()
    {
    }

    public void FileldShow()
    {
        aiCharacterView.StartScene(() =>
        {
            playerFieldView.Show();
            conversationFieldView.Show();
            informationFieldView.Show();
        });
    }

    public void StartScene(LocationType newLocation, TimeOfDay newTime, int dayCount, System.Action onComplete)
    {
        informationFieldView.SetDayCount(dayCount);
        SetTimeOfDay(newTime);
        bgImageView.StartScene(newLocation, newTime, () =>
        {
            FileldShow();
            onComplete?.Invoke();
        });
    }

    public void SetBg(LocationType newLocation, TimeOfDay newTime)
    {
        playerFieldView.StartResponseWait();
        bgImageView.ChangeBg(newLocation, newTime, () =>
        {
            playerFieldView.StartInputWait();
        });
    }

    public void AddChatCount()
    {
        informationFieldView.AddChatCount();
    }

    public void SetTimeOfDay(TimeOfDay time)
    {
        informationFieldView.SetTimeOfDay(time);
    }

    public void ChatResponse(ChatResponseViewModel viewModel)
    {
        CharacterType playCharacter = DataFacade.Instance.Character.PlayCharacter;
        //TODO:ìKêÿÇ»ViewModelÇÃéÛéÊ
        playerFieldView.ChatResponse(viewModel.PlayerOptions);
        conversationFieldView.SetAiMessage(playCharacter, viewModel.AiMessage);
        aiCharacterView.ChangeCharacter(playCharacter, viewModel.AiExpression);
        informationFieldView.AddEmotion(viewModel.AiEmotion);
    }

    public void SubscribeToChatSend(System.Action<string> listener)
    {
        playerFieldView.SubscribeToChatSend(listener);
    }

    public void SubscribeToChatLog(System.Action listener)
    {
        OnChatLogClicked += listener;
        playerFieldView.SubscribeToChatLog(OnChatLogClicked);
    }
    public void SubscribeToTimeAdvance(System.Action listener)
    {
        OnTimeAdvanceClicked += listener;
        playerFieldView.SubscribeToTimeAdvance(OnTimeAdvanceClicked);
    }
    public void SubscribeToNewOptions(System.Action listener)
    {
        OnNewOptionsClicked += listener;
        playerFieldView.SubscribeToNewOptions(OnNewOptionsClicked);
    }
}
