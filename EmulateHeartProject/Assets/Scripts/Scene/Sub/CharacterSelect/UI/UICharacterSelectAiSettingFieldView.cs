using UnityEngine;

public class UICharacterSelectAiSettingFieldView : UIFieldViewBase
{
    [SerializeField]
    private UIInputFieldView aiNameField;
    [SerializeField]
    private UIPanelShadowView aiNameShadow;
    [SerializeField]
    private UIButtonView confirmButtonView;
    [SerializeField]
    private UITextView textView;
    [SerializeField]
    private UIPanelShadowView textShadow;

    private System.Action<string> OnConfirmAction;

    private int nameMax;
    private string aiName;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        nameMax = 8;
        aiNameField.SetCharacterLimit(nameMax);
        aiNameField.SubscribeToInputTextCount(InputCheck);
        confirmButtonView.SubscribeToButtonClick(HandleConfirm);
    }

    public void SceneReset()
    {
        HideImmediate();
        aiName = "";
        aiNameField.SetText("");
        confirmButtonView.SetButtonEnabled(false);
    }

    public void SetInteractable(bool isInteractable)
    {
        confirmButtonView.SetButtonEnabled(isInteractable);
        canvasGroup.interactable = isInteractable;
    }

    public void SetCharacterType(CharacterType characterType)
    {
        aiNameShadow.SetShadowColor(characterType);
        textShadow.SetShadowColor(characterType);
    }

    private void InputCheck(int count)
    {
        if (count >= nameMax)
        {
            textView.SetText("Ç±ÇÍà»è„ì¸óÕÇ≈Ç´Ç‹ÇπÇÒ");
            textView.Show();
            return;
        }
        textView.SetText(count + " / " + nameMax);
        textView.Show();
        confirmButtonView.SetButtonEnabled(true);
    }

    private void HandleConfirm()
    {
        aiNameField.OnEndEdit();
        aiName = aiNameField.GetText();
        if (!ConfirmAiName(aiName)) return;
        OnConfirmAction?.Invoke(aiName);
    }

    private bool ConfirmAiName(string aiName)
    {
        if (string.IsNullOrWhiteSpace(aiName) || aiName.Length > nameMax)
        {
            textView.SetText("ïsê≥Ç»ì¸óÕÇ≈Ç∑");
            textView.Show();
            confirmButtonView.SetButtonEnabled(false);
            return false;
        }
        this.aiName = aiName;
        confirmButtonView.SetButtonEnabled(true);
        return true;
    }

    public void SubscribeToConfirm(System.Action<string> listener)
    {
        OnConfirmAction += listener;
    }
}
