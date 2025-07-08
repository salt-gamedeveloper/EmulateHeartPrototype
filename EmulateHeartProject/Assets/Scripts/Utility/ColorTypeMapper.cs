using System; // Enum.TryParse�̂���
using UnityEngine; // Enum�̂���

public static class ColorTypeMapper
{
    /// <summary>
    /// CharacterType��ColorPurpose�Ɋ�Â��āA�Ή�����ColorType Enum�����o�[��Ԃ��܂��B
    /// ���̃��\�b�h�́AColorType Enum�̖����K���i��: HaruTextbg, FuyuPnlShdA30�j�Ɉˑ����܂��B
    /// </summary>
    /// <param name="characterType">�L�����N�^�[�^�C�v�B</param>
    /// <param name="colorPurpose">�F�̗p�r�B</param>
    /// <returns>�Ή�����ColorType�B������Ȃ��ꍇ���͂Ɏ��s�����ꍇ��ColorType.Default��Ԃ��܂��B</returns>
    public static ColorType GetColorType(CharacterType characterType, ColorPurpose colorPurpose)
    {
        string characterPrefix = characterType.ToString(); // ��: "Haru", "Fuyu"
        string purposeSuffix;

        // ColorPurpose �ɉ����āAColorType Enum�̎��ۂ̃T�t�B�b�N�X������
        // �����Ŗ����K���̍����z������
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
            // ���� ColorPurpose �������ɒǉ����Ă���
            default:
                Debug.LogWarning($"ColorTypeMapper: Unhandled ColorPurpose '{colorPurpose}'. Cannot form ColorType name.");
                return ColorType.Default; // ����`�̏ꍇ�̓f�t�H���g��Ԃ�
        }

        // ���҂���� ColorType Enum�̕����񖼂��쐬
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
            return ColorType.Default; // ��͎��s���̓f�t�H���g��Ԃ�
        }
    }
}