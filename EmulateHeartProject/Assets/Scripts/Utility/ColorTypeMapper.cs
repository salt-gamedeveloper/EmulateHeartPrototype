using System; // Enum.TryParseのため
using UnityEngine; // Enumのため

public static class ColorTypeMapper
{
    /// <summary>
    /// CharacterTypeとColorPurposeに基づいて、対応するColorType Enumメンバーを返します。
    /// このメソッドは、ColorType Enumの命名規則（例: HaruTextbg, FuyuPnlShdA30）に依存します。
    /// </summary>
    /// <param name="characterType">キャラクタータイプ。</param>
    /// <param name="colorPurpose">色の用途。</param>
    /// <returns>対応するColorType。見つからない場合や解析に失敗した場合はColorType.Defaultを返します。</returns>
    public static ColorType GetColorType(CharacterType characterType, ColorPurpose colorPurpose)
    {
        string characterPrefix = characterType.ToString(); // 例: "Haru", "Fuyu"
        string purposeSuffix;

        // ColorPurpose に応じて、ColorType Enumの実際のサフィックスを決定
        // ここで命名規則の差を吸収する
        switch (colorPurpose)
        {
            case ColorPurpose.TextBackground:
                purposeSuffix = "Textbg";
                break;
            case ColorPurpose.Silhouette:
                purposeSuffix = "Silhouette";
                break;
            case ColorPurpose.PanelShadow:
                purposeSuffix = "PnlShdA30";
                break;
            case ColorPurpose.TextWhite:
                purposeSuffix = "TextW";
                break;
            case ColorPurpose.TextBlack:
                purposeSuffix = "TextB";
                break;
            case ColorPurpose.DecorationA:
                purposeSuffix = "DecorationA";
                break;
            case ColorPurpose.DecorationB:
                purposeSuffix = "DecorationB";
                break;
            // 他の ColorPurpose をここに追加していく
            default:
                Debug.LogWarning($"ColorTypeMapper: Unhandled ColorPurpose '{colorPurpose}'. Cannot form ColorType name.");
                return ColorType.Default; // 未定義の場合はデフォルトを返す
        }

        // 期待される ColorType Enumの文字列名を作成
        string desiredColorTypeName = characterPrefix + purposeSuffix;

        ColorType foundColorType;
        if (Enum.TryParse(desiredColorTypeName, out foundColorType))
        {
            return foundColorType;
        }
        else
        {
            Debug.LogWarning($"ColorTypeMapper: Could not parse ColorType for '{desiredColorTypeName}'. " +
                             "Check ColorType Enum definition and ColorPurpose-suffix mapping. " +
                             "Falling back to Default ColorType.");
            return ColorType.Default; // 解析失敗時はデフォルトを返す
        }
    }
}