public class SmallDialogViewModel
{
    private DialogType type;
    private string yesButtonText;
    private string noButtonText;
    private string closeButtonText;
    private string dialogText;
    private System.Action onYesAction;
    private System.Action onNoAction;
    private System.Action onCloseAction;

    public DialogType Type => type;
    public string YesButtonText => yesButtonText;
    public string NoButtonText => noButtonText;
    public string CloseButtonText => closeButtonText;
    public string DialogText => dialogText;
    public System.Action OnYesAction => onYesAction;
    public System.Action OnNoAction => onNoAction;
    public System.Action OnCloseAction => onCloseAction;

    //�e�X�g
    /*
    public SmallDialogViewModel()
    {
        type = DialogType.YesNo;
        yesButtonText = "����";
        noButtonText = "Cancel";
        dialogText = "�_�C�����O�̓��e\n���s\n���s";
        onYesAction = () => test(yesButtonText);
        onNoAction = () => test(noButtonText);
    }

    private void test(string test)
    {
        Debug.Log(test + "�������܂���");
    }
    */
    //�e�X�g

    public void CreateInfoDialogViewModel(string closeButtonText, string dialogText)
    {
        this.type = DialogType.Info;
        this.closeButtonText = closeButtonText;
        this.dialogText = dialogText;
    }

    public void CreateYesNoDialogViewModel(string yesButtonText, string noButtonText, string dialogText)
    {
        this.type = DialogType.YesNo;
        this.yesButtonText = yesButtonText;
        this.noButtonText = noButtonText;
        this.dialogText = dialogText;
    }

    public void SubscribeToYes(System.Action action)
    {
        onYesAction += action;
    }
    public void SubscribeToNo(System.Action action)
    {
        onNoAction += action;
    }
    public void SubscribeToClose(System.Action action)
    {
        onCloseAction += action;
    }
}
