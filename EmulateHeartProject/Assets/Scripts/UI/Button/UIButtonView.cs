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
    /// ボタンの有効状態を設定します。
    /// </summary>
    /// <param name="isEnabled">true にするとボタンを押せるようになり、false で無効化されます。</param>
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
            //Debug.Log("テキスト色変更");
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
    /// ボタンがクリックされたときに呼び出すリスナーを登録します。
    /// 既存のリスナーはすべて削除されます。
    /// </summary>
    /// <param name="listener">クリック時に呼び出すアクション。null の場合は何も登録しません。</param>
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

        // エディタ上で変更されたときに反映させる
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
