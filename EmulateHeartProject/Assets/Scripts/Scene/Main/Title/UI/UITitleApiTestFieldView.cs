using UnityEngine;

public class UITitleApiTestFieldView : UIFieldViewBase
{
    [SerializeField]
    private UITextEditFieldView apiKeyInputFieldView;
    [SerializeField]
    private UIButtonView apiTestButtonView;
    [SerializeField]
    private UITextView apiTestResultTextView;
    [SerializeField]
    private UIPanelShadowView textPanelShadowView;

    private System.Action OnApiTestClicked;
    private System.Action<string> OnApiKeyUpdated;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        apiTestButtonView.SubscribeToButtonClick(OnApiTestClicked);
    }
    public void SceneReset()
    {
        HideImmediate();
        StartResponseWait();
        apiKeyInputFieldView.SceneReset();
    }
    public void StartResponseWait()
    {
        canvasGroup.interactable = false;
        apiTestResultTextView.SetText("");
        apiTestResultTextView.Show();
    }

    public void StartInputWait()
    {
        canvasGroup.interactable = true;
    }

    public void SetCharacterType(CharacterType characterType)
    {
        textPanelShadowView.SetShadowColor(characterType);
    }

    public void SetApiKey(string apiKey)
    {
        apiKeyInputFieldView.SetText(apiKey);
    }

    public void SetResult(string result)
    {
        apiTestResultTextView.SetText(result);
        apiTestResultTextView.Show();
    }

    public void SubscribeToApiTest(System.Action listener)
    {
        OnApiTestClicked += listener;
        apiTestButtonView.SubscribeToButtonClick(OnApiTestClicked);
    }

    public void SubscribeToApiKeyUpdated(System.Action<string> listener)
    {
        OnApiKeyUpdated += listener;
        apiKeyInputFieldView.SubscribeToEditClick(OnApiKeyUpdated);
    }
}
