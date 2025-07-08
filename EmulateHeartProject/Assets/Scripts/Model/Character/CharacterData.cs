//TODO: ゲームスタート時に変更がある項目の取り出し
[System.Serializable]
public class CharacterData
{
    public StatusData status;
    public Profiles profiles;

    public StatusData Status => status;
    public Profiles Profiles => profiles;

    public string ToPromptString()
    {
        string statusString = "キャラクターのステータスデータがありません。";
        if (status != null)
        {
            statusString = $@"
知的探求心: {status.intellectualCuriosity}
想像力: {status.imagination}
良心性: {status.conscientiousness}
責任感: {status.responsibility}
社交性: {status.sociability}
積極性: {status.proactiveness}
共感性: {status.empathy}
思いやり: {status.compassion}
ストレス耐性: {status.stressTolerance}
不安: {status.anxiety}
知性: {status.intelligence}
人間らしさ: {status.humanLikeness}
依存度: {status.dependency}
好感度: {status.affection}
Normal発生率: {status.normalProbability}
Joy発生率: {status.joyProbability}
Anger発生率: {status.angerProbability}
Sorrow発生率: {status.sorrowProbability}
Fun発生率: {status.funProbability}";
        }

        string aiProfileString = "AIプロフィールデータがありません。";
        if (profiles?.aiProfile != null)
        {
            aiProfileString = $@"
名前: {profiles.aiProfile.name}
年齢: {profiles.aiProfile.age}
性別: {profiles.aiProfile.gender}
口調: {profiles.aiProfile.tone}
一人称: {profiles.aiProfile.firstPerson}
好きなもの: {profiles.aiProfile.favorite}
嫌いなもの: {profiles.aiProfile.hate}";
        }

        string playerProfileString = "プレイヤープロフィールデータがありません。";
        if (profiles?.playerProfile != null)
        {
            playerProfileString = $@"
名前: {profiles.playerProfile.name}
年齢: {profiles.playerProfile.age}
性別: {profiles.playerProfile.gender}
口調: {profiles.playerProfile.tone}
一人称: {profiles.playerProfile.firstPerson}
好きなもの: {profiles.playerProfile.favorite}
嫌いなもの: {profiles.playerProfile.hate}";
        }

        return $@"
【現在のAIキャラクター設定】{statusString}

【AIのプロフィール】{aiProfileString}

【プレイヤーのプロフィール】{playerProfileString}
";
    }

    public void SetAiName(string aiName)
    {
        Profiles.SetAiName(aiName);
    }

    //test
    public void debug()
    {
        status.debug();
        profiles.debug();
    }
}
