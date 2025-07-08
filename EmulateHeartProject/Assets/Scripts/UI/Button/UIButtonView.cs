using UnityEngine;
using UnityEngine.UI;

public class UIButtonView : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Button button;

    [SerializeField]
    private UITextView textView;

    [SerializeField]
    private bool isTextButton = false;

    [SerializeField]
    private ColorPaletteSO colorPalette;
    [SerializeField]
    private ColorType nomalColor = ColorType.A100;
    [SerializeField]
    private ColorType hoverColor = ColorType.Gray221;
    [SerializeField]
    private ColorType pressedColor = ColorType.Gray150;
    [SerializeField]
    private ColorType selectedColor = ColorType.A100;
    [SerializeField]
    private ColorType disabledColor = ColorType.Gray150;
    [SerializeField]
    private ColorType disabledTextColor = ColorType.Gray150;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        ApplySettings();
    }
    private void ApplySettings()
    {
        if (isTextButton && textView != null)
        {
            textView.HideImmediate();
        }

        if (button == null || colorPalette == null)
            return;

        ColorBlock updatedBlock = button.colors;
        updatedBlock.normalColor = colorPalette.GetColor(nomalColor);
        updatedBlock.highlightedColor = colorPalette.GetColor(hoverColor);
        updatedBlock.pressedColor = colorPalette.GetColor(pressedColor);
        updatedBlock.selectedColor = colorPalette.GetColor(selectedColor);
        updatedBlock.disabledColor = colorPalette.GetColor(disabledColor);
        button.colors = updatedBlock;
    }

    /// <summary>
    /// �{�^���̗L����Ԃ�ݒ肵�܂��B
    /// </summary>
    /// <param name="isEnabled">true �ɂ���ƃ{�^����������悤�ɂȂ�Afalse �Ŗ���������܂��B</param>
    public void SetButtonEnabled(bool isEnabled)
    {
        button.interactable = isEnabled;
        if (!isTextButton || textView == null)
        {
            return;
        }
        if (isEnabled)
        {
            textView.ResetColor();
        }
        else
        {
            //Debug.Log("�e�L�X�g�F�ύX");
            textView.SetTextColor(disabledTextColor);
        }
    }

    public void SetText(string text)
    {
        if (isTextButton && textView != null)
        {
            textView.SetText(text);
            textView.Show();
        }
    }

    /// <summary>
    /// �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�����X�i�[��o�^���܂��B
    /// �����̃��X�i�[�͂��ׂč폜����܂��B
    /// </summary>
    /// <param name="listener">�N���b�N���ɌĂяo���A�N�V�����Bnull �̏ꍇ�͉����o�^���܂���B</param>
    public void SubscribeToButtonClick(System.Action listener)
    {
        button.onClick.RemoveAllListeners();
        if (listener != null)
        {
            button.onClick.AddListener(() => listener());
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (button == null || colorPalette == null)
            return;

        // �G�f�B�^��ŕύX���ꂽ�Ƃ��ɔ��f������
        ColorBlock updatedBlock = button.colors;
        updatedBlock.normalColor = colorPalette.GetColor(nomalColor);
        updatedBlock.highlightedColor = colorPalette.GetColor(hoverColor);
        updatedBlock.pressedColor = colorPalette.GetColor(pressedColor);
        updatedBlock.selectedColor = colorPalette.GetColor(selectedColor);
        updatedBlock.disabledColor = colorPalette.GetColor(disabledColor);
        button.colors = updatedBlock;
    }
#endif

}
