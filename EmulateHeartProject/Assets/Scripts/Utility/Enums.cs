using System;
using System.Collections.Generic;

//SceneManager関連
public enum SceneType
{
    //更新時
    //SceneMangerの更新
    None,
    Title,
    Load,
    Settings,
    Extra,
    Story,
    Chat,
    CharacterSelect,
    Status,
    Diary,
    Save
}
public enum SceneCategory
{
    Main,
    Sub
}

public enum DialogType
{
    Info,
    YesNo,
    GeminiApi
}

//Gemini関連
public enum PromptType
{
    ChatStart,
    Chat,
    ChatTimeProgress,
    ChatRechooseOptions,
    ChatEnd,
}

//UI関連
public enum ColorType
{
    // 一般的なUIやシステムカラー
    Default = 0,
    Black95 = 1,
    Gray150 = 2,
    Gray221 = 3,
    Gray221A80 = 4, // Gray221 の透明度付きバージョン

    // 感情関連の色
    Joy = 5,
    Anger = 6,
    Sorrow = 7,
    Fun = 8,

    // キャラクターのメインカラーなど
    A100 = 9,         // 「A100」という名前が具体的な意味を持つなら変更を検討
    PlayerMain = 10,
    AiMain = 11,

    // 感情のハイライト色
    Joyhigh = 12,
    Angerhigh = 13,
    Sorrowhigh = 14,
    Funhigh = 15,

    // 透明度付きの特殊な色
    A0 = 16,
    PlayerMainhighA80 = 17,
    AiMainhighA80 = 18,

    // ハルカラー
    HaruTextbg = 101,
    HaruSilhouette = 102,
    HaruPnlShdA30 = 103,
    HaruTextW = 104,
    HaruTextB = 105,
    HaruDecorationA = 106,
    HaruDecorationB = 107,

    //ナツカラー
    NatuTextbg = 201,
    NatuSilhouette = 202,
    NatuPnlShdA30 = 203,


    //アキカラー
    AkiTextbg = 301,
    AkiSilhouette = 302,
    AkiPnlShdA30 = 303,


    //フユカラー
    FuyuTextbg = 401,
    FuyuSilhouette = 402,
    FuyuPnlShdA30 = 403,
    FuyuTextW = 404,
    FuyuTextB = 405,
    FuyuDecorationA = 406,
    FuyuDecorationB = 407,

}

public enum ColorPurpose
{
    TextBackground,
    Silhouette,
    PanelShadow,
    TextWhite,
    TextBlack,
    DecorationA,
    DecorationB,
}

public enum CharacterExpression
{
    Neutral,     // 通常・無表情（基本）
    Shd,         // 影
    Smile,       // 笑う
    Worried,     // 悩む
    Cry,         // 泣く
    Embarrassed, // 恥ずかしい
    Angry,        // 怒る
}

//ゲームシステム関連
public enum EmotionType
{
    Normal,
    Joy,
    Anger,
    Sorrow,
    Fun,
}

public enum CharacterType
{
    Player,
    Ai,
    Haru,
    Natu,
    Aki,
    Fuyu,
}

public enum LocationType
{
    None,
    Living,
    Airoom,
    Town,
    Park,
}
public enum TimeOfDay
{
    None,
    Morning,
    Afternoon,
    Evening,
    Night,
}

public enum MessageType
{
    Player,
    Ai,
    System
}

public static class Enums
{
    private static System.Random random = new System.Random();

    public static string CharacterExpressionToLowerString(CharacterExpression expression)
    {
        return expression.ToString().ToLower();
    }

    public static string CharacterTypeToLowerString(CharacterType characterType)
    {
        return characterType.ToString().ToLower();
    }

    public static string LocationTypeToLowerString(LocationType location)
    {
        return location.ToString().ToLower();
    }

    public static string TimeOfDayToLowerString(TimeOfDay time)
    {
        return time.ToString().ToLower();
    }

    public static CharacterExpression ParseCharacterExpression(string value)
    {
        if (Enum.TryParse<CharacterExpression>(value, true, out var result))
            return result;
        return CharacterExpression.Neutral;
    }

    public static string ConvertCharacterType(CharacterType characterType)
    {
        return characterType switch
        {
            CharacterType.Player => "プレイヤー",
            CharacterType.Ai => "AI",
            CharacterType.Haru => "ハル",
            CharacterType.Natu => "ナツ",
            CharacterType.Aki => "アキ",
            CharacterType.Fuyu => "フユ",
            _ => "不明"
        };
    }

    /// <summary>
    /// Haru, Natu, Aki, Fuyu の中からランダムな CharacterType を取得します。
    /// </summary>
    /// <returns>ランダムに選択された CharacterType (Haru, Natu, Aki, Fuyu のいずれか)。</returns>
    public static CharacterType GetRandomAiCharacterType()
    {
        List<CharacterType> selectableTypes = new List<CharacterType>
        {
            CharacterType.Haru,
            CharacterType.Natu,
            CharacterType.Aki,
            CharacterType.Fuyu
        };

        if (selectableTypes.Count == 0)
        {
            UnityEngine.Debug.LogWarning("GetRandomAiCharacterType: ランダム選択可能なAIキャラクタータイプが見つかりませんでした。");
            return CharacterType.Haru;
        }

        int randomIndex = random.Next(0, selectableTypes.Count);
        return selectableTypes[randomIndex];
    }
}
