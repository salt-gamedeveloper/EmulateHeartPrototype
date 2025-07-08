public class SubCoommonViewModel : ISceneViewModel
{
    private string sceneName;
    private string iconFileId;

    public string SceneName => sceneName;
    public string IconFileId => iconFileId;

    public SubCoommonViewModel(string sceneName, string iconFileId)
    {
        this.sceneName = sceneName;
        this.iconFileId = iconFileId;
    }
}
