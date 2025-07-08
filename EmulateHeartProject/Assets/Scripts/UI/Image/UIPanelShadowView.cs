using UnityEngine;

public class UIPanelShadowView : MonoBehaviour
{
    [SerializeField]
    private UIImageView panelImageView;

    public void SetShadowColor(CharacterType characterType)
    {
        ColorType colorType = ColorTypeMapper.GetColorType(characterType, ColorPurpose.PanelShadow);
        panelImageView.SetColor(colorType);
        Debug.Log("ShadowColor : " + colorType.ToString());
    }
}
