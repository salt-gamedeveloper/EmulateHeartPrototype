using System.IO;
using UnityEditor;
using UnityEngine;


public class ColorGeneratorEditor : EditorWindow
{
    private static string csvFilePath = "Assets/Editor/colors.csv";
    private static string outputFolder = "Assets/SO/Color/Colors";

    [MenuItem("Tools/Theme/Generate Colors from CSV")]
    public static void ShowWindow()
    {
        GetWindow<ColorGeneratorEditor>("Generate Colors");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("CSV����ColorSO�𐶐�", EditorStyles.boldLabel);

        csvFilePath = EditorGUILayout.TextField("CSV File Path", csvFilePath);
        outputFolder = EditorGUILayout.TextField("Output Folder", outputFolder);

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate ColorSOs"))
        {
            GenerateColorSOs();
        }

        EditorGUILayout.HelpBox("CSV�t�@�C���̓��e�Ɋ�Â��āAColorSO�A�Z�b�g�𐶐��܂��͍X�V���܂��B\n" +
                                "CSV�t�H�[�}�b�g: ColorType(Enum��),RGBColor(RRGGBB)\n" +
                                "��: HaruTextbg,F3FFEB\n" +
                                "����: �����̃A�Z�b�g�͏㏑������܂��B�A���t�@�l�͎蓮�Őݒ肵�Ă��������B", UnityEditor.MessageType.Info);
    }

    private static void GenerateColorSOs()
    {
        if (!AssetDatabase.IsValidFolder(outputFolder))
        {
            Directory.CreateDirectory(Application.dataPath + outputFolder.Replace("Assets", ""));
            AssetDatabase.Refresh();
        }

        if (!File.Exists(csvFilePath))
        {
            EditorUtility.DisplayDialog("�G���[", "CSV�t�@�C����������܂���: " + csvFilePath, "OK");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        int generatedCount = 0;
        int skippedCount = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("//") || line.StartsWith("#")) continue;
            if (line.StartsWith("ColorType,RGBColor")) continue; // �w�b�_�[�s���C��

            string[] parts = line.Split(',');

            if (parts.Length != 2)
            {
                Debug.LogWarning($"CSV�̍s�t�H�[�}�b�g���s���ł�: {line} (�X�L�b�v)");
                skippedCount++;
                continue;
            }

            string colorTypeString = parts[0].Trim();
            string rgbColorString = parts[1].Trim(); // HexColor ���� RGBColor �ɖ��O�ύX

            ColorType colorType;
            if (!System.Enum.TryParse(colorTypeString, out colorType))
            {
                Debug.LogWarning($"������ ColorType �ł�: {colorTypeString} (�s: {line})�BColorType Enum�ɑ��݂��邩�m�F���Ă��������B(�X�L�b�v)");
                skippedCount++;
                continue;
            }

            Color colorValue;
            // RGB (6��) �݂̂�z��B�A���t�@�l�͂����ŋ����I��1.0�ɐݒ�
            if (!ColorUtility.TryParseHtmlString("#" + rgbColorString, out colorValue))
            {
                Debug.LogWarning($"������RGBColor�R�[�h�ł�: {rgbColorString} (�s: {line})�B(�X�L�b�v)");
                skippedCount++;
                continue;
            }

            // ���������d�v: �A���t�@�l�͂����ŏ�ɕs�����i1.0�j�ɐݒ�
            // ���[�U�[���蓮�Œ������邱�Ƃ�O��Ƃ���
            colorValue.a = 1.0f;

            string assetPath = Path.Combine(outputFolder, colorType.ToString() + ".asset");
            ColorSO existingColorSO = AssetDatabase.LoadAssetAtPath<ColorSO>(assetPath);

            if (existingColorSO == null)
            {
                ColorSO newColorSO = ScriptableObject.CreateInstance<ColorSO>();
                newColorSO.GetType().GetField("colorType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(newColorSO, colorType);
                newColorSO.GetType().GetField("colorValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(newColorSO, colorValue);

                AssetDatabase.CreateAsset(newColorSO, assetPath);
                generatedCount++;
            }
            else
            {
                existingColorSO.GetType().GetField("colorType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(existingColorSO, colorType);
                // ������SO�̃A���t�@�l���㏑�����Ȃ��ꍇ�́A�����̃A���t�@�l��ێ����郍�W�b�N���K�v
                // �������ACSV��RGB�݂̂Ȃ�A�V����RGB�l��ݒ肵�A�A���t�@�͎蓮�ݒ�ɔC����̂��V���v��
                existingColorSO.GetType().GetField("colorValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(existingColorSO, colorValue);
                EditorUtility.SetDirty(existingColorSO);
                generatedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("��������", $"{generatedCount}��ColorSO�𐶐�/�X�V���܂����B\n{skippedCount}�̍s���X�L�b�v����܂����B", "OK");
    }
}