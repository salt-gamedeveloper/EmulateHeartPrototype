using UnityEngine;

[System.Serializable]
public class ProfileData
{
    //���O
    public string name;
    //�N��
    public string age;
    //����
    public string gender;
    //����
    public string tone;
    //��l��
    public string firstPerson;
    //�D���Ȃ���
    public string favorite;
    //�����Ȃ���
    public string hate;

    public string Name => name;
    public string Age => age;
    public string Gender => gender;
    public string Tone => tone;
    public string FirstPerson => firstPerson;
    public string Favorite => favorite;
    public string Hate => hate;

    //test
    public void debug()
    {
        Debug.Log(
            $"ProfileData:\n" +
            $"���O: {name}\n" +
            $"�N��: {age}\n" +
            $"����: {gender}\n" +
            $"����: {tone}\n" +
            $"��l��: {firstPerson}\n" +
            $"�D���Ȃ���: {favorite}\n" +
            $"�����Ȃ���: {hate}"
        );
    }

    public void SetName(string name)
    {
        this.name = name;
    }
}
