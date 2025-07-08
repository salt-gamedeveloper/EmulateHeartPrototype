using UnityEditor;

[CustomEditor(typeof(ImageDataSO))]
public class ImageDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ImageDataSO imageDataSO = (ImageDataSO)target;

        // IDの前後空白を削除
        string trimmedId = imageDataSO.Id?.Trim();

        if (string.IsNullOrEmpty(trimmedId))
            return;

        // トリム結果が違ったらSOのIDも更新して保存
        if (imageDataSO.Id != trimmedId)
        {
            SerializedProperty idProp = serializedObject.FindProperty("id");
            if (idProp != null)
            {
                idProp.stringValue = trimmedId;
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(imageDataSO);
            }
        }

        string path = AssetDatabase.GetAssetPath(imageDataSO);
        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

        // ファイル名がIDと違ったらファイル名をIDに変更
        if (fileName != trimmedId)
        {
            AssetDatabase.RenameAsset(path, trimmedId);

            imageDataSO.name = trimmedId;
            EditorUtility.SetDirty(imageDataSO);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}