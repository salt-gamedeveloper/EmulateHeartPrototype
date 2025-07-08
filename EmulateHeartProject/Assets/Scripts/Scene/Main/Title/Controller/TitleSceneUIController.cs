using UnityEngine;

public class TitleSceneUIController : UIControllerBase
{
    //テスト用

    [SerializeField]
    private UIButtonView testButtonView;
    private System.Action OnTest;
    public void SubscribeToTest(System.Action listener)
    {
        OnTest += listener;
        testButtonView.SubscribeToButtonClick(() => OnTest?.Invoke());
    }

    //テスト用

    [SerializeField]
    private UIBGImageView bgImageView;
    [SerializeField]
    private UICharacterImageView characterView;
    [SerializeField]
    private UITitleApiTestFieldView apiTestFieldView;
    [SerializeField]
    private UITitleButtonsFieldView buttonsFieldView;

    [SerializeField]
    private UIButtonView newGameButtonView;
    [SerializeField]
    private UIButtonView loadGameButtonView;
    [SerializeField]
    private UIButtonView extraButtonView;
    [SerializeField]
    private UIButtonView settingButtonView;
    [SerializeField]
    private UIButtonView exitButtonView;
    [SerializeField]
    private UIButtonView apiKeyInfo;

    private System.Action OnApiTestClicked;
    private System.Action<string> OnApiKeyUpdated;

    private System.Action OnNewGameClicked;
    private System.Action OnLoadGameClicked;
    private System.Action OnExtraClicked;
    private System.Action OnSettingClicked;
    private System.Action OnExitClicked;
    private System.Action OnApiKeyInfo;


    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        OnApiTestClicked += StartResponseWait;
        Close();
    }
    public void SceneReset()
    {
        bgImageView.SceneReset();
        apiTestFieldView.SceneReset();
        characterView.SceneReset();
        buttonsFieldView.SceneReset();
        newGameButtonView.SetButtonEnabled(false);
        loadGameButtonView.SetButtonEnabled(false);
    }

    public void Open()
    {
        Show();
    }

    public void Close()
    {
        Hide();
    }

    public void FileldShow()
    {
        characterView.StartScene(() =>
        {
            apiTestFieldView.Show();
            buttonsFieldView.Show();
        });
    }

    public void StartScene(LocationType newLocation, TimeOfDay newTime, string apiKey, CharacterType character, System.Action onComplete)
    {
        apiTestFieldView.SetApiKey(apiKey);
        characterView.SetCharacterType(character);
        apiTestFieldView.SetCharacterType(character);
        bgImageView.StartScene(newLocation, newTime, () =>
        {
            FileldShow();
            onComplete?.Invoke();
        });
    }

    public void TestResponse(bool result, string resultText)
    {
        apiTestFieldView.SetResult(resultText);
        apiTestFieldView.StartInputWait();

        //test
        testButtonView.SetButtonEnabled(result);

        newGameButtonView.SetButtonEnabled(result);
        loadGameButtonView.SetButtonEnabled(result);
        extraButtonView.SetButtonEnabled(true);
        settingButtonView.SetButtonEnabled(true);
    }

    private void StartResponseWait()
    {
        apiTestFieldView.StartResponseWait();
        //test
        testButtonView.SetButtonEnabled(false);

        newGameButtonView.SetButtonEnabled(false);
        loadGameButtonView.SetButtonEnabled(false);
        extraButtonView.SetButtonEnabled(false);
        settingButtonView.SetButtonEnabled(false);
    }

    public void SubscribeToApiTest(System.Action listener)
    {
        OnApiTestClicked += listener;
        apiTestFieldView.SubscribeToApiTest(OnApiTestClicked);
    }

    public void SubscribeToApiKeyUpdated(System.Action<string> listener)
    {
        OnApiKeyUpdated += listener;
        apiTestFieldView.SubscribeToApiKeyUpdated(OnApiKeyUpdated);
    }

    public void SubscribeToNewGame(System.Action listener)
    {
        OnNewGameClicked += listener;
        newGameButtonView.SubscribeToButtonClick(() => OnNewGameClicked?.Invoke());
    }
    public void SubscribeToLoadGame(System.Action listener)
    {
        OnLoadGameClicked += listener;
        loadGameButtonView.SubscribeToButtonClick(() => OnLoadGameClicked?.Invoke());
    }
    public void SubscribeToExtra(System.Action listener)
    {
        OnExtraClicked += listener;
        extraButtonView.SubscribeToButtonClick(() => OnExtraClicked?.Invoke());
    }

    public void SubscribeToSetting(System.Action listener)
    {
        OnSettingClicked += listener;
        settingButtonView.SubscribeToButtonClick(() => OnSettingClicked?.Invoke());
    }

    public void SubscribeToExit(System.Action listener)
    {
        OnExitClicked += listener;
        exitButtonView.SubscribeToButtonClick(() => OnExitClicked?.Invoke());
    }

    public void SubscribeToApiKeyInfo(System.Action listener)
    {
        OnApiKeyInfo = listener;
        apiKeyInfo.SubscribeToButtonClick(() => OnApiKeyInfo?.Invoke());
    }
}
