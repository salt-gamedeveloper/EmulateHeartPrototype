public class DiarySceneUIController : UIControllerBase
{
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        Close();
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
