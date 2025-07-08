using System.Collections;
using UnityEngine;

//FIXME:ChatCount、TimeProgress周りでうまく動かないタイミングがある
public class ChatSceneFlowController : MonoBehaviour, ISceneFlowController
{
    //テスト
    private System.Action GoToTitle;

    public void SubscribeToGoToTitle(System.Action listener)
    {
        GoToTitle = listener;
    }
    //テスト
    SceneType sceneType = SceneType.Chat;
    [SerializeField]
    private ChatSceneUIController controller;
    [SerializeField]
    private GeminiApiManager apiManager;

    private ChatContextViewModel chatContextViewModel;

    private LocationType locationType;
    private TimeOfDay timeOfDay;
    private int dayCount;

    private float scenarioDelay;
    private int chatMax;
    private int chatCount;

    private int joyCount;
    private int angerCount;
    private int sorrowCount;
    private int funCount;

    private System.Action OnChatLogClicked;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        scenarioDelay = 5f;
        chatMax = 10;
        chatCount = 0;

        controller.SubscribeToChatSend(ChatSend);
        controller.SubscribeToTimeAdvance(OnTimeAdvanceClicked);
        //TODO:選択肢再考は調整中
        //controller.SubscribeToNewOptions(OnNewOptionsClicked);
        CloseScene();

    }
    public void OpenScene()
    {
        joyCount = 0;
        angerCount = 0;
        sorrowCount = 0;
        funCount = 0;

        controller.SceneReset();
        //TODO:スタートデータの取得

        GameTime gameTime = DataFacade.Instance.Context.GetGamaTime();

        //テスト
        chatContextViewModel = new ChatContextViewModel(LocationType.Airoom, gameTime.TimeOfDay, gameTime.DayCount);
        //テスト

        locationType = chatContextViewModel.Location;
        timeOfDay = chatContextViewModel.Time;
        dayCount = chatContextViewModel.Day;

        controller.Open();
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(scenarioDelay);
        controller.SceneReset();
        controller.StartScene(locationType, timeOfDay, dayCount, () =>
          {
              ChatStart();
          });
    }

    public void CloseScene()
    {
        controller.Close();
    }
    public void ShowScene()
    {
        controller.Show();
    }
    public void HideScene()
    {
        controller.Hide();
    }

    public void ReopenScene()
    {

    }

    public SceneType GetSceneType() { return sceneType; }

    private void ChatStart()
    {
        apiManager.ChatStartGeminiConnection(GeminiResponse);
    }

    public void ChatSend(string chatText)
    {
        PromptType promptType = AddChatCount();
        if (promptType == PromptType.ChatEnd)
        {
            Debug.Log("一日の終了");
            //GoToTitle?.Invoke();
            //TODO: リザルトへの移行
            return;
        }

        apiManager.RequestGeminiConnection(promptType, chatText, GeminiResponse);
        MessageData messageData = new MessageData(MessageType.Player, chatText);

        DataFacade.Instance.Conversation.AddSceneChat(messageData);
    }

    public void ChatResponse(ChatResponseViewModel viewModel)
    {
        controller.ChatResponse(viewModel);

        MessageData messageData = new MessageData(MessageType.Ai, viewModel.AiMessage);
        DataFacade.Instance.Conversation.AddSceneChat(messageData);
        string aiPrevious = $"\"aiExpression\": \"{viewModel.AiExpression}\",\n\"aiMessage\": \"{viewModel.AiMessage}\"";
        Debug.Log("Emo" + viewModel.AiEmotion.ToString());

        switch (viewModel.AiEmotion)
        {
            case EmotionType.Joy:
                joyCount++;
                break;
            case EmotionType.Anger:
                angerCount++;
                break;
            case EmotionType.Sorrow:
                sorrowCount++;
                break;
            case EmotionType.Fun:
                funCount++;
                break;
            default:
                break;
        }
    }

    private PromptType AddChatCount()
    {
        PromptType promptType = PromptType.Chat;
        chatCount++;
        if (chatCount >= chatMax)
        {
            promptType = TimeProgress();
        }
        else
        {
            controller.AddChatCount();
        }
        return promptType;
    }

    public PromptType TimeProgress()
    {
        chatCount = 0;
        Debug.Log("[FlowController] 時間を進める処理を呼び出しました");

        //データマネージャーの時間を進める
        var result = DataFacade.Instance.Context.AdvanceTime();

        timeOfDay = result.timeOfDay;
        bool isNewDay = result.isNewDay;

        if (isNewDay)
        {
            GoToTitle?.Invoke();
            return PromptType.ChatEnd;
        }

        controller.SetTimeOfDay(timeOfDay);
        controller.SetBg(locationType, timeOfDay);

        SceneChatHistory chatHistory = DataFacade.Instance.Conversation.GetSceneChatHistory();
        string test = chatHistory.ToChatPromptString();
        Debug.Log("直近10件" + test);

        return PromptType.ChatTimeProgress;
    }
    private void GeminiResponse(bool result, ChatResponseDTO chatResponseDTO)
    {

        if (!result)
        {
            //TODO : エラー処理
            Debug.Log("[chatFlow] 通信失敗");
            return;
        }

        ChatResponseViewModel viewModel = new ChatResponseViewModel(chatResponseDTO);
        ChatResponse(viewModel);
        DataFacade.Instance.Character.UpdateCharacter(chatResponseDTO.Character);
    }
    public void OnTimeAdvanceClicked()
    {
        Debug.Log("[FlowController] 時間を進めるボタンが押されました");
        TimeProgress();
    }

    public void OnNewOptionsClicked()
    {
        Debug.Log("[FlowController] 他の選択肢を考えるボタンが押されました");
    }

    public void SubscribeToChatLog(System.Action listener)
    {
        OnChatLogClicked += listener;
        controller.SubscribeToChatLog(OnChatLogClicked);
    }
}
