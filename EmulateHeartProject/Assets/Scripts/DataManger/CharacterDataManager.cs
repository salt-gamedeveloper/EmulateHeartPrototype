using System;

//TODO: playCharacterの管理を別クラスで行う
//TODO: ロード時用のcharacterData更新メソッドの作成
public class CharacterDataManager
{

    private CharacterData characterData;

    private CharacterType playCharacter;
    public CharacterType PlayCharacter => playCharacter;

    public CharacterDataManager()
    {
        characterData = JsonLoader.LoadFromResources<CharacterData>("Json/Character/Haru_character_data");
        playCharacter = CharacterType.Haru;
    }

    //TODO: 新規ゲーム開始時の流れをまとめる
    public void CharacterSelect(CharacterType character, string aiName)
    {
        characterData = JsonLoader.LoadFromResources<CharacterData>("Json/Character/" + character.ToString() + "_character_data");
        characterData.SetAiName(aiName);
        playCharacter = character;
    }

    public void UpdateCharacter(CharacterData diff)
    {
        MergeCharacterDiff(characterData, diff);
    }

    private void MergeCharacterDiff(CharacterData target, CharacterData diff)
    {
        if (diff.status != null)
        {
            var statusType = typeof(StatusData);
            foreach (var field in statusType.GetFields())
            {
                var targetValue = (int)field.GetValue(target.status); // 元の値をTargeから取得
                var newValue = (int)field.GetValue(diff.status);

                if (Math.Abs(newValue - targetValue) > 5)
                {

                    continue;
                }

                field.SetValue(target.status, newValue);
            }
        }

        if (diff.profiles != null)
        {
            if (diff.profiles.aiProfile != null)
            {
                var profileType = typeof(ProfileData);
                foreach (var field in profileType.GetFields())
                {
                    var newValue = (string)field.GetValue(diff.profiles.aiProfile);
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        field.SetValue(target.profiles.aiProfile, newValue);
                    }
                }
            }

            if (diff.profiles.playerProfile != null)
            {
                var profileType = typeof(ProfileData);
                foreach (var field in profileType.GetFields())
                {
                    var newValue = (string)field.GetValue(diff.profiles.playerProfile);
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        field.SetValue(target.profiles.playerProfile, newValue);
                    }
                }
            }
        }
    }

    public CharacterData GetCharacter()
    {
        characterData.Status.debug();
        return characterData;
    }
}
