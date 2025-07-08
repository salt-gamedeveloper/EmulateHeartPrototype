using UnityEditor;

[CustomEditor(typeof(ImageDataSO))]
public class ImageDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ImageDataSO imageDataSO = (ImageDataSO)target;

        // ID�̑O��󔒂��폜
        string trimmedId = imageDataSO.Id?.Trim();

        if (string.IsNullOrEmpty(trimmedId))
            return;

        // �g�������ʂ��������SO��ID���X�V���ĕۑ�
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

        // �t�@�C������ID�ƈ������t�@�C������ID�ɕύX
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