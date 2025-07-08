using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "Theme/Color")]
public class ColorSO : ScriptableObject
{
    [SerializeField]
    private ColorType colorType;
    [SerializeField]
    private Color colorValue;

    public ColorType ColorType => colorType;
    public Color ColorValue => colorValue;

    private void OnValidate()
    {
        if (name != colorType.ToString())
        {
            name = colorType.ToString();
        }
    }
}
