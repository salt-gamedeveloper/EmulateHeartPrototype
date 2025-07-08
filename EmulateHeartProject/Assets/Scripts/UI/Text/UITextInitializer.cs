using TMPro;
using UnityEngine;

public class UITextInitializer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textLabel;
    [SerializeField]
    private ColorPaletteSO colorPalette;

    [Header("初期化設定")]
    [SerializeField]
    private ColorType textColor;

    private Color initialColor;

    public Color InitialColor => initialColor;

    private void Awake()
    {
        if (textLabel != null || colorPalette != null)
        {
            Initialize();
        }
        else
        {
            Debug.LogWarning("UITextInitializer: textLabelかcolorPaletteがセットされていません。");
        }
    }

    private void Initialize()
    {
        initialColor = colorPalette.GetColor(textColor);
        textLabel.color = initialColor;
    }

    public void ResetColor()
    {
        if (textLabel != null)
        {
            textLabel.color = initialColor;
        }
    }

    public void SetColor(ColorType colorType)
    {
        if (textLabel != null && colorPalette != null)
        {
            textLabel.color = colorPalette.GetColor(colorType);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyTextColor();
    }

    private void ApplyTextColor()
    {
        if (textLabel != null && colorPalette != null)
        {
            Color color = colorPalette.GetColor(textColor);
            textLabel.color = color;
            initialColor = color;
            UnityEditor.EditorUtility.SetDirty(textLabel);
        }
    }
#endif
}
