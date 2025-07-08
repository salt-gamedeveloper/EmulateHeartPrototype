using System.IO;
using UnityEditor;
using UnityEngine;

public class ImageDataCreator : EditorWindow
{
    private string csvFilePath = "Assets/Data/_mapping.csv";
    private string spriteFolder = "Assets/Art/";
    private string outputSOFolder = "Assets/SO/Image/ImageDatas/";

    [MenuItem("Tools/ImageData Creator")]
    public static void ShowWindow() => GetWindow<ImageDataCreator>("ImageData Creator");

    private void OnGUI()
    {
        csvFilePath = EditorGUILayout.TextField("CSV Path", csvFilePath);
        spriteFolder = EditorGUILayout.TextField("Sprite Folder", spriteFolder);
        outputSOFolder = EditorGUILayout.TextField("Output SO Folder", outputSOFolder);

        if (GUILayout.Button("Create ImageDataSO"))
        {
            CreateImageDataFromCSV();
        }
    }

    private void CreateImageDataFromCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSVファイルが見つかりません: " + csvFilePath);
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Debug.LogWarning($"列不足: {line}");
                continue;
            }

            string fileName = parts[0].Trim();
            string unityID = parts[1].Trim();

            Sprite sprite = FindSpriteByName(fileName);
            if (sprite == null)
            {
                Debug.LogWarning($"Spriteが見つかりません: {fileName}");
                continue;
            }

            string soPath = $"{outputSOFolder}/{unityID}.asset";

            var existingSO = AssetDatabase.LoadAssetAtPath<ImageDataSO>(soPath);
            if (existingSO != null)
            {
                Debug.Log($"既存SOが存在します: {unityID}");
                continue;
            }

            var so = ScriptableObject.CreateInstance<ImageDataSO>();
            so.name = unityID;
            var soSerialized = new SerializedObject(so);
            soSerialized.FindProperty("id").stringValue = unityID;
            soSerialized.FindProperty("sprite").objectReferenceValue = sprite;
            soSerialized.ApplyModifiedProperties();

            AssetDatabase.CreateAsset(so, soPath);
            EditorUtility.SetDirty(so);

            Debug.Log($"ImageDataSO作成: {unityID}");
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private Sprite FindSpriteByName(string name)
    {
        var guids = AssetDatabase.FindAssets($"{name} t:Sprite", new[] { spriteFolder });
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string filename = Path.GetFileNameWithoutExtension(path);
            if (filename == name)
            {
                return AssetDatabase.LoadAssetAtPath<Sprite>(path);
            }
        }
        return null;
    }
}
