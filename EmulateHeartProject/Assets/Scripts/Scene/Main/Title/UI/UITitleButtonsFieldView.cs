public class UITitleButtonsFieldView : UIFieldViewBase
{
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
    }
    public void SceneReset()
    {
        HideImmediate();
    }

    public void StartResponseWait()
    {
        canvasGroup.interactable = false;
    }

    public void StartInputWait()
    {
        canvasGroup.interactable = true;
    }
}
