using UnityEditor;

[CustomEditor(typeof(ColorSO))]
public class ColorSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorSO colorSO = (ColorSO)target;

        // SO�̖��O��colorType�ƈ������t�@�C������ύX����
        string desiredName = colorSO.ColorType.ToString();

        string path = AssetDatabase.GetAssetPath(colorSO);
        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

        if (fileName != desiredName)
        {
            string newPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), desiredName + ".asset");
            AssetDatabase.RenameAsset(path, desiredName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}