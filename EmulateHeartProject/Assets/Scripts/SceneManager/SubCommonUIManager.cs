using UnityEngine;

public class SubCommonUIManager : MonoBehaviour
{
    [SerializeField]
    private SubCommonUIController subCommonUIController;

    private System.Action onSceneCloseClicked;

    private SubCoommonViewModel commonViewModel;

    private SubCommonViewModelFactory subCommonViewModelFactory;

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        subCommonViewModelFactory = new SubCommonViewModelFactory();
    }

    public void SetCommonUI(SceneType sceneType)
    {
        commonViewModel = subCommonViewModelFactory.Create(sceneType);
        subCommonUIController.InjectViewModel(commonViewModel);
    }

    public void Open()
    {
        subCommonUIController.Open();
    }

    public void Close()
    {
        subCommonUIController.Close();
    }

    public void SetupButtonCallbacks()
    {
        subCommonUIController.SubscribeToSceneClose(() => onSceneCloseClicked?.Invoke());
        subCommonUIController.SetupButtonCallbacks();
    }

    public void SubscribeToSceneClose(System.Action listener) => onSceneCloseClicked += listener;
}
