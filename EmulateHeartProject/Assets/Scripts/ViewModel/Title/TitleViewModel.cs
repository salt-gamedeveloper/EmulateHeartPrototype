public class TitleViewModel : ISceneViewModel
{
    private bool _isNewGameEnabled;
    private bool _isLoadGameEnabled;

    public bool IsNewGameEnabled => _isNewGameEnabled;
    public bool IsLoadGameEnabled => _isLoadGameEnabled;
    public TitleViewModel(bool isNewGameEnable, bool isLoadGameEnabled)
    {
        _isNewGameEnabled = isNewGameEnable;
        _isLoadGameEnabled = isLoadGameEnabled;
    }
}
