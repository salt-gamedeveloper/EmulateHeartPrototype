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
            // DTOがnullの場合のエラーハンドリング
            // ここでデフォルト値やエラーメッセージを設定することができます。
            this.aiMessage = "エラー: 応答データがありません。";
            this.aiExpression = CharacterExpression.Neutral; // デフォルトの表情
            this.aiEmotion = EmotionType.Normal;
            List<PlayerOption> playerOptions = new List<PlayerOption>();
            playerOptions.Add(new PlayerOption(EmotionType.Normal, "選択肢がありません"));
            Debug.LogError("ChatResponseDTO is null when creating ViewModel.");
            return;
        }
        aiExpression = Enums.ParseCharacterExpression(dto.AiExpression);
        aiEmotion = dto.aiEmotion;
        aiMessage = dto.AiMessage;
        playerOptions = dto.PlayerOptions;
    }

    /*
    //テスト
    public ChatResponseViewModel(List<PlayerOption> playerOptions, string aiMessage)
    {
        this.playerOptions = playerOptions;
        this.aiMessage = aiMessage;
    }

    public static ChatResponseViewModel test()
    {
        List<PlayerOption> options = new List<PlayerOption>();
        options.Add(new PlayerOption(EmotionType.Normal, "こんにちは"));
        options.Add(new PlayerOption(EmotionType.Joy, "はじめまして!"));
        options.Add(new PlayerOption(EmotionType.Sorrow, "..."));
        string message = "はじめまして";
        return new ChatResponseViewModel(options,message);
    }
    //テスト
    */
}
