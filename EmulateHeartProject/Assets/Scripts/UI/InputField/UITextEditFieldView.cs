using UnityEngine;

public class UITextEditFieldView : MonoBehaviour
{

    [SerializeField]
    private UIButtonView editButtonView;
    [SerializeField]
    private UIImageChanger changer;
    [SerializeField]
    private UIInputFieldView inputFieldView;


    //trueなら編集可能
    private bool isEditing;


    private event System.Action<string> OnTextEditEnded;

    // テスト用
    /*
    void Start()
    {
        StartCoroutine(SkipAfterDelay(3f));

    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Show();
    }
    */
    //テスト用

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        isEditing = false;
        editButtonView.SubscribeToButtonClick(OnEditButton);
        UpdateInputFieldStates();
    }

    public void SceneReset()
    {
        isEditing = false;
        UpdateInputFieldStates();
    }

    public void SetText(string text)
    {
        inputFieldView.SetText(text);
    }

    private void OnEditButton()
    {
        if (isEditing)
        {
            inputFieldView.OnEndEdit();
            OnTextEditEnded?.Invoke(inputFieldView.GetText());
        }
        isEditing = !isEditing;
        UpdateInputFieldStates();
        UpdateIcon();
    }

    private void UpdateInputFieldStates()
    {
        inputFieldView.SetMasked(!isEditing);
        inputFieldView.SetInputEnabled(isEditing);
    }

    private void UpdateIcon()
    {
        if (isEditing)
        {
            changer.ChangeImage("text_edit_check");
        }
        else
        {
            changer.ChangeImage("text_edit_pen");
        }
    }

    public void SetCharacterLimit(int limit)
    {
        inputFieldView.SetCharacterLimit(limit);
    }

    public string GetText()
    {
        inputFieldView.OnEndEdit();
        return inputFieldView.GetText();
    }

    public void SubscribeToEditClick(System.Action<string> listener)
    {
        OnTextEditEnded += listener;
    }

    public void SubscribeToInputTextCount(System.Action<int> listener)
    {
        inputFieldView.SubscribeToInputTextCount(listener);
    }
}
