using System;
using System.Collections.Generic;

//SceneManager�֘A
public enum SceneType
{
    //�X�V��
    //SceneManger�̍X�V
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

//Gemini�֘A
public enum PromptType
{
    ChatStart,
    Chat,
    ChatTimeProgress,
    ChatRechooseOptions,
    ChatEnd,
}

//UI�֘A
public enum ColorType
{
    // ��ʓI��UI��V�X�e���J���[
    Default = 0,
    Black95 = 1,
    Gray150 = 2,
    Gray221 = 3,
    Gray221A80 = 4, // Gray221 �̓����x�t���o�[�W����

    // ����֘A�̐F
    Joy = 5,
    Anger = 6,
    Sorrow = 7,
    Fun = 8,

    // �L�����N�^�[�̃��C���J���[�Ȃ�
    A100 = 9,         // �uA100�v�Ƃ������O����̓I�ȈӖ������Ȃ�ύX������
    PlayerMain = 10,
    AiMain = 11,

    // ����̃n�C���C�g�F
    Joyhigh = 12,
    Angerhigh = 13,
    Sorrowhigh = 14,
    Funhigh = 15,

    // �����x�t���̓���ȐF
    A0 = 16,
    PlayerMainhighA80 = 17,
    AiMainhighA80 = 18,

    // �n���J���[
    HaruTextbg = 101,
    HaruSilhouette = 102,
    HaruPnlShdA30 = 103,
    HaruTextW = 104,
    HaruTextB = 105,
    HaruDecorationA = 106,
    HaruDecorationB = 107,

    //�i�c�J���[
    NatuTextbg = 201,
    NatuSilhouette = 202,
    NatuPnlShdA30 = 203,


    //�A�L�J���[
    AkiTextbg = 301,
    AkiSilhouette = 302,
    AkiPnlShdA30 = 303,


    //�t���J���[
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
    Neutral,     // �ʏ�E���\��i��{�j
    Shd,         // �e
    Smile,       // �΂�
    Worried,     // �Y��
    Cry,         // ����
    Embarrassed, // �p��������
    Angry,        // �{��
}

//�Q�[���V�X�e���֘A
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
            CharacterType.Player => "�v���C���[",
            CharacterType.Ai => "AI",
            CharacterType.Haru => "�n��",
            CharacterType.Natu => "�i�c",
            CharacterType.Aki => "�A�L",
            CharacterType.Fuyu => "�t��",
            _ => "�s��"
        };
    }

    /// <summary>
    /// Haru, Natu, Aki, Fuyu �̒����烉���_���� CharacterType ���擾���܂��B
    /// </summary>
    /// <returns>�����_���ɑI�����ꂽ CharacterType (Haru, Natu, Aki, Fuyu �̂����ꂩ)�B</returns>
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
            UnityEngine.Debug.LogWarning("GetRandomAiCharacterType: �����_���I���\��AI�L�����N�^�[�^�C�v��������܂���ł����B");
            return CharacterType.Haru;
        }

        int randomIndex = random.Next(0, selectableTypes.Count);
        return selectableTypes[randomIndex];
    }
}
