using UnityEngine;

//TODO: 再設計。分離する
public class DialogCanvasController : MonoBehaviour
{
    [SerializeField]
    private UIButtonView bgButtonView;
    [SerializeField]
    private UIChatLogFieldView chatLogView;
    [SerializeField]
    private UISideMenuFieldView sideMenuView;
    [SerializeField]
    private UISmallDialogFieldView smallDialogView;

    private System.Action OnChatLogCloseAction;

    private System.Action OnSideMenuCloseAction;
    private System.Action OnSideMenuSceneChangeAction;

    private System.Action OnDialogYesAction;
    private System.Action OnDialogNoAction;
    private System.Action OnDialogCloseAction;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {

    }

    public void OpenChatToLog()
    {
        SceneChatHistory sceneChatHistory = DataFacade.Instance.Conversation.GetSceneChatHistory();
        ChatLogViewModel viewModel = new ChatLogViewModel(sceneChatHistory, "タイトル");
        OpenChatLog(viewModel);
    }
    private void OpenChatLog(ChatLogViewModel viewModel)
    {
        chatLogView.InjectViewModel(viewModel);
        chatLogView.Show();

        chatLogView.SubscribeToDialogClose(HandleChatLogClose);
        bgButtonView.SubscribeToButtonClick(HandleChatLogClose);
    }
    public void OpenSideMenu()
    {
        sideMenuView.Show();

        sideMenuView.SubscribeToLoadGame(HandleSideMenuSceneChange);
        sideMenuView.SubscribeToStatus(HandleSideMenuSceneChange);
        sideMenuView.SubscribeToDiary(HandleSideMenuSceneChange);
        sideMenuView.SubscribeToSetting(HandleSideMenuSceneChange);

        sideMenuView.SubscribeToDialogClose(HandleSideMenuClose);
        bgButtonView.SubscribeToButtonClick(HandleSideMenuClose);
    }

    public void OpenSmallDialog(SmallDialogViewModel viewModel)
    {
        switch (viewModel.Type)
        {
            case DialogType.Info:
                viewModel.SubscribeToClose(HandleDialogClose);
                bgButtonView.SubscribeToButtonClick(HandleDialogClose);
                break;
            case DialogType.YesNo:
                viewModel.SubscribeToYes(HandleYes);
                viewModel.SubscribeToNo(HandleNo);
                bgButtonView.SubscribeToButtonClick(HandleNo);
                break;
            default:
                Debug.LogError("不正なDialogTypeです");
                return;
        }
        smallDialogView.InjectViewModel(viewModel);
        smallDialogView.Show();
    }

    public void SubscribeToDialogYes(System.Action callback)
    {
        OnDialogYesAction = callback;
    }

    public void SubscribeToDialogNo(System.Action callback)
    {
        OnDialogNoAction = callback;
    }

    public void SubscribeToDialogClose(System.Action callback)
    {
        OnDialogCloseAction = callback;
    }

    public void SubscribeToChatLogClose(System.Action callback)
    {
        OnChatLogCloseAction = callback;
    }

    public void SubscribeToSideMenuClose(System.Action callback)
    {
        OnSideMenuCloseAction = callback;
    }

    public void SubscribeToSideMenuSceneChange(System.Action callback)
    {
        OnSideMenuSceneChangeAction = callback;
    }

    private void HandleYes()
    {
        OnDialogYesAction?.Invoke();
        ResetCallbacks();
    }

    private void HandleNo()
    {
        OnDialogNoAction?.Invoke();
        ResetCallbacks();
    }

    private void HandleDialogClose()
    {
        OnDialogCloseAction?.Invoke();
        ResetCallbacks();
    }

    private void HandleChatLogClose()
    {
        OnChatLogCloseAction?.Invoke();
        ResetCallbacks();
    }

    private void HandleSideMenuClose()
    {
        OnSideMenuCloseAction?.Invoke();
        ResetCallbacks();
    }

    private void HandleSideMenuSceneChange()
    {
        OnSideMenuSceneChangeAction?.Invoke();
        ResetCallbacks();
    }

    public void CloseAllDialogs(System.Action onComplete)
    {
        int pendingCount = 3;

        void OnOneComplete()
        {
            pendingCount--;
            if (pendingCount == 0)
            {
                onComplete?.Invoke();
            }
        }
        chatLogView.HideWithCallback(OnOneComplete);
        sideMenuView.HideWithCallback(OnOneComplete);
        smallDialogView.HideWithCallback(OnOneComplete);

        ResetCallbacks();
    }


    private void ResetCallbacks()
    {
        OnDialogYesAction = null;
        OnDialogNoAction = null;
        OnDialogCloseAction = null;
        OnChatLogCloseAction = null;
        OnSideMenuCloseAction = null;
        OnSideMenuSceneChangeAction = null;
    }
}
