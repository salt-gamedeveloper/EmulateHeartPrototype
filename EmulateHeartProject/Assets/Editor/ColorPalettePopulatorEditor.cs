using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorPalettePopulatorEditor : EditorWindow
{
    private static string colorSOsFolder = "Assets/SO/Color/Colors"; // ColorSOアセットが格納されているフォルダ
    private static ColorPaletteSO targetColorPaletteSO; // 流し込み先のColorPaletteSOアセット

    [MenuItem("Tools/Theme/Populate Color Palette")] // メニュー項目名を変更
    public static void ShowWindow()
    {
        GetWindow<ColorPalettePopulatorEditor>("Populate Color Palette");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("ColorPaletteSOにColorSOを登録", EditorStyles.boldLabel);

        // ColorSOアセットが格納されているフォルダを指定
        colorSOsFolder = EditorGUILayout.TextField("ColorSOs Folder", colorSOsFolder);

        // 流し込み先のColorPaletteSOアセットをオブジェクトフィールドで指定
        targetColorPaletteSO = (ColorPaletteSO)EditorGUILayout.ObjectField(
            "Target Color Palette",
            targetColorPaletteSO,
            typeof(ColorPaletteSO),
            false // シーン内オブジェクトではないので false
        );

        EditorGUILayout.Space();

        if (GUILayout.Button("Populate Palette with ColorSOs")) // ボタン名を変更
        {
            PopulateColorPalette(); // メソッド名を変更
        }

        EditorGUILayout.HelpBox("指定されたフォルダ内のすべてのColorSOアセットを、\n" +
                                "指定されたColorPaletteSOに自動的に登録します。\n" +
                                "ColorPaletteSOの既存リストはクリアされ、再構築されます。", UnityEditor.MessageType.Info);
    }

    private static void PopulateColorPalette()
    {
        if (targetColorPaletteSO == null)
        {
            EditorUtility.DisplayDialog("エラー", "ColorPaletteSOが指定されていません。", "OK");
            return;
        }

        if (!AssetDatabase.IsValidFolder(colorSOsFolder))
        {
            EditorUtility.DisplayDialog("エラー", "ColorSOアセットのフォルダが見つかりません: " + colorSOsFolder, "OK");
            return;
        }

        // 指定フォルダ内のすべてのColorSOアセットを検索
        // AssetDatabase.FindAssets は GUID を返すため、AssetDatabase.LoadAssetAtPath でロード
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
            EditorUtility.DisplayDialog("情報", "指定されたフォルダにColorSOアセットが見つかりませんでした。", "OK");
            return;
        }

        // ColorPaletteSO のリストを更新
        targetColorPaletteSO.colors.Clear(); // 一旦クリア
        targetColorPaletteSO.colors.AddRange(foundColorSOs); // 検索されたSOを追加

        // ColorPaletteSO の変更をダーティとしてマークし、保存を促す
        EditorUtility.SetDirty(targetColorPaletteSO);
        AssetDatabase.SaveAssets(); // アセットの変更をディスクに保存
        AssetDatabase.Refresh();    // アセットデータベースを更新

        EditorUtility.DisplayDialog("パレット登録完了",
                                    $"{foundColorSOs.Count}個のColorSOをColorPaletteSOに登録しました。", "OK");
    }
}