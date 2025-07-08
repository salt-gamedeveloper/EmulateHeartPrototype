using UnityEngine;

public class SubCommonUIController : UIControllerBase, IUIController<SubCoommonViewModel>
{
    [SerializeField]
    private UITextView sceneNameTextView;
    [SerializeField]
    private UIButtonView closeButtonView;

    private event System.Action onSceneCloseButtonClicked;

    private string sceneName;
    private string iconFileId;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Hide();
        sceneName = "sceneName";
        iconFileId = "icon_setting";
    }

    public void InjectViewModel(SubCoommonViewModel viewModel)
    {
        sceneName = viewModel.SceneName;
        iconFileId = viewModel.IconFileId;
    }

    void IUIController.InjectViewModel(object viewModel)
    {
        InjectViewModel((SubCoommonViewModel)viewModel);
    }

    public void Open()
    {
        Show();

        sceneNameTextView.SetText(sceneName);

        sceneNameTextView.Show();
    }

    public void Close()
    {
        Hide();
    }
    public void SetupButtonCallbacks()
    {
        closeButtonView.SubscribeToButtonClick(() => onSceneCloseButtonClicked?.Invoke());
    }

    public void SubscribeToSceneClose(System.Action listener) => onSceneCloseButtonClicked += listener;

}
