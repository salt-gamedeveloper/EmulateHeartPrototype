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
        EditorGUILayout.LabelField("CSVからColorSOを生成", EditorStyles.boldLabel);

        csvFilePath = EditorGUILayout.TextField("CSV File Path", csvFilePath);
        outputFolder = EditorGUILayout.TextField("Output Folder", outputFolder);

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate ColorSOs"))
        {
            GenerateColorSOs();
        }

        EditorGUILayout.HelpBox("CSVファイルの内容に基づいて、ColorSOアセットを生成または更新します。\n" +
                                "CSVフォーマット: ColorType(Enum名),RGBColor(RRGGBB)\n" +
                                "例: HaruTextbg,F3FFEB\n" +
                                "注意: 既存のアセットは上書きされます。アルファ値は手動で設定してください。", UnityEditor.MessageType.Info);
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
            EditorUtility.DisplayDialog("エラー", "CSVファイルが見つかりません: " + csvFilePath, "OK");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        int generatedCount = 0;
        int skippedCount = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("//") || line.StartsWith("#")) continue;
            if (line.StartsWith("ColorType,RGBColor")) continue; // ヘッダー行を修正

            string[] parts = line.Split(',');

            if (parts.Length != 2)
            {
                Debug.LogWarning($"CSVの行フォーマットが不正です: {line} (スキップ)");
                skippedCount++;
                continue;
            }

            string colorTypeString = parts[0].Trim();
            string rgbColorString = parts[1].Trim(); // HexColor から RGBColor に名前変更

            ColorType colorType;
            if (!System.Enum.TryParse(colorTypeString, out colorType))
            {
                Debug.LogWarning($"無効な ColorType です: {colorTypeString} (行: {line})。ColorType Enumに存在するか確認してください。(スキップ)");
                skippedCount++;
                continue;
            }

            Color colorValue;
            // RGB (6桁) のみを想定。アルファ値はここで強制的に1.0に設定
            if (!ColorUtility.TryParseHtmlString("#" + rgbColorString, out colorValue))
            {
                Debug.LogWarning($"無効なRGBColorコードです: {rgbColorString} (行: {line})。(スキップ)");
                skippedCount++;
                continue;
            }

            // ★ここが重要: アルファ値はここで常に不透明（1.0）に設定
            // ユーザーが手動で調整することを前提とする
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
                // 既存のSOのアルファ値を上書きしない場合は、既存のアルファ値を保持するロジックが必要
                // しかし、CSVがRGBのみなら、新しいRGB値を設定し、アルファは手動設定に任せるのがシンプル
                existingColorSO.GetType().GetField("colorValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(existingColorSO, colorValue);
                EditorUtility.SetDirty(existingColorSO);
                generatedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("生成完了", $"{generatedCount}個のColorSOを生成/更新しました。\n{skippedCount}個の行がスキップされました。", "OK");
    }
}