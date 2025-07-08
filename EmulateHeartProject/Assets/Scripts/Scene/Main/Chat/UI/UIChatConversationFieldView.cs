using UnityEngine;

public class UIChatConversationFieldView : UIFieldViewBase
{
    [SerializeField]
    private UITextView aiMessegeTextView;
    [SerializeField]
    private UIPanelShadowView panelShadowView;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
    }

    public void SceneReset()
    {
        //HACK: �s�K�؂�CharacterType�̎Q��
        //FIXME: �V�K�L�����N�^�[�̐F���Q�Ƃ���Ȃ�
        CharacterType character = DataFacade.Instance.Character.PlayCharacter;
        panelShadowView.SetShadowColor(character);
        aiMessegeTextView.SetText("");
        aiMessegeTextView.Show();
        HideImmediate();
    }

    public void SetAiMessage(CharacterType character, string message)
    {
        panelShadowView.SetShadowColor(character);
        aiMessegeTextView.SetText(message);
        aiMessegeTextView.Show();
    }
}
