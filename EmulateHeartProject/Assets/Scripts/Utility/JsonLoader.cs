using System;
using UnityEngine;

public static class JsonLoader
{
    /// <summary>
    /// Resources �t�H���_���� JSON �t�@�C����ǂݍ��݁AT�^�̃I�u�W�F�N�g�Ƀf�V���A���C�Y���܂��B
    /// </summary>
    /// <typeparam name="T">�f�V���A���C�Y�Ώۂ̌^</typeparam>
    /// <param name="fileName">Resources �t�H���_���� JSON �t�@�C�����i�g���q�s�v�j</param>
    /// <returns>�ǂݍ��� T �^�̃C���X�^���X�B���s�����ꍇ�� null�B</returns>
    public static T LoadFromResources<T>(string fileName) where T : class
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile == null)
        {
            Debug.LogError($"[JsonLoader] Resources/{fileName}.json ��������܂���ł����B");
            return null;
        }

        try
        {
            T data = JsonUtility.FromJson<T>(jsonFile.text);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"[JsonLoader] JSON �̓ǂݍ��ݒ��ɃG���[���������܂���: {e.Message}");
            return null;
        }
    }
}
