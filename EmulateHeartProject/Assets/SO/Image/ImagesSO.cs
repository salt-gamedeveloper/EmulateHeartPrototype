using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Images", menuName = "Image/ImageDataList")]
public class ImagesSO : ScriptableObject
{
    [SerializeField] private List<ImageDataSO> imageList;

    private Dictionary<string, ImageDataSO> lookupCache;

    private void OnEnable()
    {
        lookupCache = new Dictionary<string, ImageDataSO>();

        foreach (var data in imageList)
        {
            if (data != null && !string.IsNullOrEmpty(data.Id))
            {
                lookupCache[data.Id] = data;
            }
        }
    }

    public ImageDataSO GetById(string id)
    {
        if (lookupCache.TryGetValue(id, out var result))
        {
            return result;
        }
        Debug.LogWarning($"ID '{id}' に一致する ImageDataSO が見つかりません");
        return null;
    }

    public bool AddImageData(ImageDataSO imageData)
    {
        if (imageData == null || string.IsNullOrEmpty(imageData.Id)) return false;

        if (lookupCache == null)
            lookupCache = new Dictionary<string, ImageDataSO>();

        if (imageList == null)
            imageList = new List<ImageDataSO>();

        if (!lookupCache.ContainsKey(imageData.Id))
        {
            imageList.Add(imageData);
            lookupCache[imageData.Id] = imageData;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
            return true;  // 追加成功
        }
        return false;  // 既に存在して追加しなかった
    }

}