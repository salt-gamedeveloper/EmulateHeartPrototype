using UnityEngine;

[System.Serializable]
public class ProfileData
{
    //名前
    public string name;
    //年齢
    public string age;
    //性別
    public string gender;
    //口調
    public string tone;
    //一人称
    public string firstPerson;
    //好きなもの
    public string favorite;
    //嫌いなもの
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
            $"名前: {name}\n" +
            $"年齢: {age}\n" +
            $"性別: {gender}\n" +
            $"口調: {tone}\n" +
            $"一人称: {firstPerson}\n" +
            $"好きなもの: {favorite}\n" +
            $"嫌いなもの: {hate}"
        );
    }

    public void SetName(string name)
    {
        this.name = name;
    }
}
