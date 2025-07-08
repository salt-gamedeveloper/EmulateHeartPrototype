using System.Collections.Generic;
using UnityEngine;

public class ChatResponseViewModel
{
    private List<PlayerOption> playerOptions;
    private CharacterExpression aiExpression;
    private EmotionType aiEmotion;
    private string aiMessage;

    public List<PlayerOption> PlayerOptions => playerOptions;
    public CharacterExpression AiExpression => aiExpression;
    public EmotionType AiEmotion => aiEmotion;
    public string AiMessage => aiMessage;

    public ChatResponseViewModel(ChatResponseDTO dto)
    {
        if (dto == null)
        {
            // DTO��null�̏ꍇ�̃G���[�n���h�����O
            // �����Ńf�t�H���g�l��G���[���b�Z�[�W��ݒ肷�邱�Ƃ��ł��܂��B
            this.aiMessage = "�G���[: �����f�[�^������܂���B";
            this.aiExpression = CharacterExpression.Neutral; // �f�t�H���g�̕\��
            this.aiEmotion = EmotionType.Normal;
            List<PlayerOption> playerOptions = new List<PlayerOption>();
            playerOptions.Add(new PlayerOption(EmotionType.Normal, "�I����������܂���"));
            Debug.LogError("ChatResponseDTO is null when creating ViewModel.");
            return;
        }
        aiExpression = Enums.ParseCharacterExpression(dto.AiExpression);
        aiEmotion = dto.aiEmotion;
        aiMessage = dto.AiMessage;
        playerOptions = dto.PlayerOptions;
    }

    /*
    //�e�X�g
    public ChatResponseViewModel(List<PlayerOption> playerOptions, string aiMessage)
    {
        this.playerOptions = playerOptions;
        this.aiMessage = aiMessage;
    }

    public static ChatResponseViewModel test()
    {
        List<PlayerOption> options = new List<PlayerOption>();
        options.Add(new PlayerOption(EmotionType.Normal, "����ɂ���"));
        options.Add(new PlayerOption(EmotionType.Joy, "�͂��߂܂���!"));
        options.Add(new PlayerOption(EmotionType.Sorrow, "..."));
        string message = "�͂��߂܂���";
        return new ChatResponseViewModel(options,message);
    }
    //�e�X�g
    */
}
