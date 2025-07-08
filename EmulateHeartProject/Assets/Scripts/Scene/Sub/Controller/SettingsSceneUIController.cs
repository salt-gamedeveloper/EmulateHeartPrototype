public class SettingsSceneUIController : UIControllerBase, IUIController<SettingsViewModel>
{
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        Close();
    }
    public void InjectViewModel(SettingsViewModel viewModel)
    {
    }
    void IUIController.InjectViewModel(object viewModel)
    {
        InjectViewModel((SettingsViewModel)viewModel);
    }
    public void Open()
    {
        Show();
    }
    public void Close()
    {
        Hide();
    }
}
