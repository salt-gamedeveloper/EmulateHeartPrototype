using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInputFieldView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private UITextView placeholderView;
    [SerializeField]
    private UITextView textView;
    [SerializeField]
    private bool isMaskEnabled = true;
    [TextArea]
    [SerializeField]
    private string placeholderText = "";

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

    //マスク用で使用
    private int visibleLength = 3;
    private string originalText;
    private bool isMasked;
    private bool isInputEnabled = true;

    private System.Action<int> OnCharacterCountChanged;
    private System.Action OnShiftEnter;

    // テスト用
    /*
    void Start()
    {
        StartCoroutine(SkipAfterDelay(1f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Show();
        UpdateDisplayedText();
    }
    */
    //テスト用

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        // 入力欄がフォーカスされていて、入力可能状態のときだけ反応
        if (isInputEnabled && inputField.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                OnShiftEnter?.Invoke();
            }
        }
    }

    private void Initialize()
    {
        isMasked = false;
        SetInputEnabled(true);

        inputField.text = "";
        originalText = "";
        placeholderView.SetText(placeholderText);
        placeholderView.SetTextColor(ColorType.Gray150);
        placeholderView.Show();
        textView.SetTextColor(ColorType.Black95);

        inputField.onValueChanged.AddListener(OnValueChanged);

        UpdateDisplayedText();
    }

    public void SetInputEnabled(bool isEnabled)
    {
        isInputEnabled = isEnabled;
        inputField.interactable = isEnabled;
        inputField.readOnly = !isEnabled;
    }

    public void SetMasked(bool mask)
    {
        isMasked = mask && isMaskEnabled;
        UpdateDisplayedText();
    }
    public void SetText(string text)
    {
        originalText = text;
        inputField.text = text;
        UpdateDisplayedText();
    }

    public void SetCharacterLimit(int limit)
    {
        inputField.characterLimit = limit;
    }

    public void SetPlaceholderText(string placeholder)
    {
        placeholderText = placeholder;
        placeholderView.SetText(placeholderText);
        placeholderView.Show();
    }

    public string GetText()
    {
        return originalText;
    }

    public void OnEndEdit()
    {
        originalText = inputField.text;
    }

    public void FocusInput()
    {
        inputField.ActivateInputField();
    }

    private void UpdateDisplayedText()
    {
        //Debug.Log($"[InputField] text = '{inputField.text}'");
        if (isMasked)
        {
            int maskLength = Mathf.Max(0, originalText.Length - visibleLength);
            string visiblePart = originalText.Substring(0, Mathf.Min(visibleLength, originalText.Length));
            string maskedPart = new string('*', maskLength);
            inputField.text = visiblePart + maskedPart;
        }
        else
        {
            inputField.text = originalText;
        }
    }

    private void OnValueChanged(string newText)
    {
        // 外部に通知（購読者がいるなら）
        OnCharacterCountChanged?.Invoke(newText.Length);
    }

    public void SubscribeToShiftEnter(System.Action listener)
    {
        OnShiftEnter = listener;
    }

    public void SubscribeToInputTextCount(System.Action<int> listener)
    {
        OnCharacterCountChanged = listener;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (inputField == null || colorPalette == null)
            return;

        // エディタ上で変更されたときに反映させる
        ColorBlock updatedBlock = inputField.colors;
        updatedBlock.normalColor = colorPalette.GetColor(nomalColor);
        updatedBlock.highlightedColor = colorPalette.GetColor(hoverColor);
        updatedBlock.pressedColor = colorPalette.GetColor(pressedColor);
        updatedBlock.selectedColor = colorPalette.GetColor(selectedColor);
        updatedBlock.disabledColor = colorPalette.GetColor(disabledColor);
        inputField.colors = updatedBlock;
    }
#endif
}
