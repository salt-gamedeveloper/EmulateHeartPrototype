using System;
using UnityEngine;

public static class JsonLoader
{
    /// <summary>
    /// Resources フォルダから JSON ファイルを読み込み、T型のオブジェクトにデシリアライズします。
    /// </summary>
    /// <typeparam name="T">デシリアライズ対象の型</typeparam>
    /// <param name="fileName">Resources フォルダ内の JSON ファイル名（拡張子不要）</param>
    /// <returns>読み込んだ T 型のインスタンス。失敗した場合は null。</returns>
    public static T LoadFromResources<T>(string fileName) where T : class
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile == null)
        {
            Debug.LogError($"[JsonLoader] Resources/{fileName}.json が見つかりませんでした。");
            return null;
        }

        try
        {
            T data = JsonUtility.FromJson<T>(jsonFile.text);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"[JsonLoader] JSON の読み込み中にエラーが発生しました: {e.Message}");
            return null;
        }
    }
}
