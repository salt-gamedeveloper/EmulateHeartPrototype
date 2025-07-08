using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorPalettePopulatorEditor : EditorWindow
{
    private static string colorSOsFolder = "Assets/SO/Color/Colors"; // ColorSO�A�Z�b�g���i�[����Ă���t�H���_
    private static ColorPaletteSO targetColorPaletteSO; // �������ݐ��ColorPaletteSO�A�Z�b�g

    [MenuItem("Tools/Theme/Populate Color Palette")] // ���j���[���ږ���ύX
    public static void ShowWindow()
    {
        GetWindow<ColorPalettePopulatorEditor>("Populate Color Palette");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("ColorPaletteSO��ColorSO��o�^", EditorStyles.boldLabel);

        // ColorSO�A�Z�b�g���i�[����Ă���t�H���_���w��
        colorSOsFolder = EditorGUILayout.TextField("ColorSOs Folder", colorSOsFolder);

        // �������ݐ��ColorPaletteSO�A�Z�b�g���I�u�W�F�N�g�t�B�[���h�Ŏw��
        targetColorPaletteSO = (ColorPaletteSO)EditorGUILayout.ObjectField(
            "Target Color Palette",
            targetColorPaletteSO,
            typeof(ColorPaletteSO),
            false // �V�[�����I�u�W�F�N�g�ł͂Ȃ��̂� false
        );

        EditorGUILayout.Space();

        if (GUILayout.Button("Populate Palette with ColorSOs")) // �{�^������ύX
        {
            PopulateColorPalette(); // ���\�b�h����ύX
        }

        EditorGUILayout.HelpBox("�w�肳�ꂽ�t�H���_���̂��ׂĂ�ColorSO�A�Z�b�g���A\n" +
                                "�w�肳�ꂽColorPaletteSO�Ɏ����I�ɓo�^���܂��B\n" +
                                "ColorPaletteSO�̊������X�g�̓N���A����A�č\�z����܂��B", UnityEditor.MessageType.Info);
    }

    private static void PopulateColorPalette()
    {
        if (targetColorPaletteSO == null)
        {
            EditorUtility.DisplayDialog("�G���[", "ColorPaletteSO���w�肳��Ă��܂���B", "OK");
            return;
        }

        if (!AssetDatabase.IsValidFolder(colorSOsFolder))
        {
            EditorUtility.DisplayDialog("�G���[", "ColorSO�A�Z�b�g�̃t�H���_��������܂���: " + colorSOsFolder, "OK");
            return;
        }

        // �w��t�H���_���̂��ׂĂ�ColorSO�A�Z�b�g������
        // AssetDatabase.FindAssets �� GUID ��Ԃ����߁AAssetDatabase.LoadAssetAtPath �Ń��[�h
        string[] guids = AssetDatabase.FindAssets("t:ColorSO", new[] { colorSOsFolder });
        List<ColorSO> foundColorSOs = new List<ColorSO>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ColorSO colorSO = AssetDatabase.LoadAssetAtPath<ColorSO>(path);
            if (colorSO != null)
            {
                foundColorSOs.Add(colorSO);
            }
        }

        if (foundColorSOs.Count == 0)
        {
            EditorUtility.DisplayDialog("���", "�w�肳�ꂽ�t�H���_��ColorSO�A�Z�b�g��������܂���ł����B", "OK");
            return;
        }

        // ColorPaletteSO �̃��X�g���X�V
        targetColorPaletteSO.colors.Clear(); // ��U�N���A
        targetColorPaletteSO.colors.AddRange(foundColorSOs); // �������ꂽSO��ǉ�

        // ColorPaletteSO �̕ύX���_�[�e�B�Ƃ��ă}�[�N���A�ۑ��𑣂�
        EditorUtility.SetDirty(targetColorPaletteSO);
        AssetDatabase.SaveAssets(); // �A�Z�b�g�̕ύX���f�B�X�N�ɕۑ�
        AssetDatabase.Refresh();    // �A�Z�b�g�f�[�^�x�[�X���X�V

        EditorUtility.DisplayDialog("�p���b�g�o�^����",
                                    $"{foundColorSOs.Count}��ColorSO��ColorPaletteSO�ɓo�^���܂����B", "OK");
    }
}