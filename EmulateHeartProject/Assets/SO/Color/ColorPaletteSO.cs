using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPaletteSO", menuName = "Theme/Color Palette")]
public class ColorPaletteSO : ScriptableObject
{
    public List<ColorSO> colors;
    private Dictionary<ColorType, Color> colorMap;

    private void OnEnable()
    {
        colorMap = new Dictionary<ColorType, Color>();

        foreach (var c in colors)
        {
            if (!colorMap.ContainsKey(c.ColorType))
                colorMap.Add(c.ColorType, c.ColorValue);
        }
    }

    public Color GetColor(ColorType type, Color fallback = default)
    {
        if (colorMap != null && colorMap.TryGetValue(type, out var color))
        {
            return color;
        }

        if (fallback == default)
        {
            fallback = Color.black;
        }

        return fallback;
    }
}

