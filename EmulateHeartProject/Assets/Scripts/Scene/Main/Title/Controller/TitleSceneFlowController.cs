using System.Collections;
using UnityEngine;

//FIXME:�r���h�����APIKey�̃}�X�N���O��ăX�^�[�g����
public class TitleSceneFlowController : MonoBehaviour, ISceneFlowController
{
    //�e�X�g�p

    private System.Action OnTest;
    public void SubscribeToTest(System.Action listener)
    {
        OnTest += listener;
        controller.SubscribeToTest(OnTest);
    }

    //�e�X�g�p

    SceneType sceneType = SceneType.Title;
    [SerializeField]
    private TitleSceneUIController controller;
    [SerializeField]
    private GeminiApiManager apiManager;

    private TitleViewModel titleViewModel;
    private string apiKey;

    private LocationType locationType;
    private TimeOfDay timeOfDay;
    private CharacterType characterType;

    private float scenarioDelay;

    private System.Action onNewGameClicked;
    private System.Action onLoadGameClicked;
    private System.Action onExtraClicked;
    private System.Action onSettingClicked;
    private System.Action onExitClicked;
    private System.Action OnApiKeyInfo;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        scenarioDelay = 0.5f;
        apiKey = "";
        controller.SubscribeToApiTest(ApiTestStart);
        controller.SubscribeToApiKeyUpdated(SetApiKey);
    }

    public void OpenScene()
    {
        DataFacade.Instance.DataReset();
        //TODO: �K�؂�ViewModel�擾
        controller.SceneReset();



        locationType = LocationType.Airoom;
        timeOfDay = TimeOfDay.Morning;
        characterType = Enums.GetRandomAiCharacterType();


        controller.Open();
        StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(scenarioDelay);
        controller.StartScene(locationType, timeOfDay, apiKey, characterType, () =>
           {
               ApiTestStart();
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

    public SceneType GetSceneType()
    {
        return sceneType;
    }

    private void ApiTestStart()
    {
        Debug.Log(apiKey);
        apiManager.SetGeminiApiKey(apiKey, GeminiResponse);
    }
    private void GeminiResponse(bool result, string resultText)
    {
        if (!result)
        {
            //TODO: �G���[����
            Debug.Log("[chatFlow] �ʐM���s");
            controller.TestResponse(result, "�ʐM���s\nAPIKey���ʐM�����m�F���Ă�������");
            return;
        }
        controller.TestResponse(result, resultText);
    }

    private void SetApiKey(string apiKey)
    {
        this.apiKey = apiKey;

    }

    public void SubscribeToNewGame(System.Action listener)
    {
        onNewGameClicked += listener;
        controller.SubscribeToNewGame(() => onNewGameClicked?.Invoke());
    }
    public void SubscribeToLoadGame(System.Action listener)
    {
        onLoadGameClicked += listener;
        controller.SubscribeToLoadGame(() => onLoadGameClicked?.Invoke());
    }
    public void SubscribeToExtra(System.Action listener)
    {
        onExtraClicked += listener;
        controller.SubscribeToExtra(() => onExtraClicked?.Invoke());
    }

    public void SubscribeToSetting(System.Action listener)
    {
        onSettingClicked += listener;
        controller.SubscribeToSetting(() => onSettingClicked?.Invoke());
    }

    public void SubscribeToExit(System.Action listener)
    {
        onExitClicked += listener;
        controller.SubscribeToExit(() => onExitClicked?.Invoke());
    }

    public void SubscribeToApiKeyInfo(System.Action listener)
    {
        OnApiKeyInfo = listener;
        controller.SubscribeToApiKeyInfo(() => OnApiKeyInfo?.Invoke());
    }

}