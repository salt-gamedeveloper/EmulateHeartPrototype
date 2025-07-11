using UnityEditor;
using UnityEngine;

public class ImagesSOInjector : EditorWindow
{
    private ImagesSO imagesSO;
    private string imageDataFolder = "Assets/SO/Image/ImageDatas/";

    [MenuItem("Tools/ImagesSO Injector")]
    public static void ShowWindow() => GetWindow<ImagesSOInjector>("ImagesSO Injector");

    private void OnGUI()
    {
        imagesSO = (ImagesSO)EditorGUILayout.ObjectField("Target ImagesSO", imagesSO, typeof(ImagesSO), false);
        imageDataFolder = EditorGUILayout.TextField("ImageData Folder", imageDataFolder);

        if (GUILayout.Button("Inject ImageDataSO"))
        {
            InjectImageDataToImagesSO();
        }
    }

    private void InjectImageDataToImagesSO()
    {
        if (imagesSO == null)
        {
            Debug.LogError("ImagesSOがセットされていません");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:ImageDataSO", new[] { imageDataFolder });
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var so = AssetDatabase.LoadAssetAtPath<ImageDataSO>(path);

            if (so == null) continue;

            if (imagesSO.AddImageData(so))
            {
                EditorUtility.SetDirty(imagesSO);
                Debug.Log($"ImagesSOに追加: {so.name}");
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
