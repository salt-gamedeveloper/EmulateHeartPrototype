using UnityEngine;
using UnityEngine.UI;

public class UIImageInitializer : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private ColorPaletteSO colorPalette;
    [SerializeField]
    private ImageDataSO imageData;

    [Header("�������ݒ�")]
    [SerializeField]
    private ColorType imageColor;

    private Color initialColor;
    public Color InitialColor => initialColor;

    private void Awake()
    {
        if (targetImage != null && colorPalette != null)
        {
            Initialize();
        }
        else
        {
            Debug.LogWarning("UIImageInitializer: targetImage��colorPalette���Z�b�g����Ă��܂���B");
        }
    }

    private void Initialize()
    {
        initialColor = colorPalette.GetColor(imageColor);
        targetImage.color = initialColor;
    }

    public void ResetColor()
    {
        if (targetImage != null)
        {
            targetImage.color = initialColor;
        }
    }

    public void SetColor(ColorType colorType)
    {
        if (targetImage == null)
        {
            Debug.LogWarning($"UIImageInitializer: SetColor({colorType})���s����targetImage��null�ł��B", this);
            return;
        }
        if (colorPalette == null)
        {
            Debug.LogWarning($"UIImageInitializer: SetColor({colorType})���s����colorPalette��null�ł��B", this);
            return;
        }


        if (targetImage != null && colorPalette != null)
        {
            initialColor = colorPalette.GetColor(colorType);
            targetImage.color = initialColor;
            //Debug.Log($"UIImageInitializer: �F�� {colorType} �ɐݒ肵�܂����B", this);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyImageFromSO();
        ApplyColorFromPalette();
    }

    private void ApplyImageFromSO()
    {
        if (targetImage != null && imageData != null)
        {
            targetImage.sprite = imageData.Sprite;
            UnityEditor.EditorUtility.SetDirty(targetImage);
        }
    }
    private void ApplyColorFromPalette()
    {
        if (targetImage != null && colorPalette != null)
        {
            Color color = colorPalette.GetColor(imageColor);
            targetImage.color = color;
            initialColor = color;
            UnityEditor.EditorUtility.SetDirty(targetImage);
        }
    }
#endif
}
