//TODO: �Q�[���X�^�[�g���ɕύX�����鍀�ڂ̎��o��
[System.Serializable]
public class CharacterData
{
    public StatusData status;
    public Profiles profiles;

    public StatusData Status => status;
    public Profiles Profiles => profiles;

    public string ToPromptString()
    {
        string statusString = "�L�����N�^�[�̃X�e�[�^�X�f�[�^������܂���B";
        if (status != null)
        {
            statusString = $@"
�m�I�T���S: {status.intellectualCuriosity}
�z����: {status.imagination}
�ǐS��: {status.conscientiousness}
�ӔC��: {status.responsibility}
�Ќ�: {status.sociability}
�ϋɐ�: {status.proactiveness}
������: {status.empathy}
�v�����: {status.compassion}
�X�g���X�ϐ�: {status.stressTolerance}
�s��: {status.anxiety}
�m��: {status.intelligence}
�l�Ԃ炵��: {status.humanLikeness}
�ˑ��x: {status.dependency}
�D���x: {status.affection}
Normal������: {status.normalProbability}
Joy������: {status.joyProbability}
Anger������: {status.angerProbability}
Sorrow������: {status.sorrowProbability}
Fun������: {status.funProbability}";
        }

        string aiProfileString = "AI�v���t�B�[���f�[�^������܂���B";
        if (profiles?.aiProfile != null)
        {
            aiProfileString = $@"
���O: {profiles.aiProfile.name}
�N��: {profiles.aiProfile.age}
����: {profiles.aiProfile.gender}
����: {profiles.aiProfile.tone}
��l��: {profiles.aiProfile.firstPerson}
�D���Ȃ���: {profiles.aiProfile.favorite}
�����Ȃ���: {profiles.aiProfile.hate}";
        }

        string playerProfileString = "�v���C���[�v���t�B�[���f�[�^������܂���B";
        if (profiles?.playerProfile != null)
        {
            playerProfileString = $@"
���O: {profiles.playerProfile.name}
�N��: {profiles.playerProfile.age}
����: {profiles.playerProfile.gender}
����: {profiles.playerProfile.tone}
��l��: {profiles.playerProfile.firstPerson}
�D���Ȃ���: {profiles.playerProfile.favorite}
�����Ȃ���: {profiles.playerProfile.hate}";
        }

        return $@"
�y���݂�AI�L�����N�^�[�ݒ�z{statusString}

�yAI�̃v���t�B�[���z{aiProfileString}

�y�v���C���[�̃v���t�B�[���z{playerProfileString}
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
