using System;
using System.Collections.Generic;

[Serializable]
public class ChatResponseDTO
{
    //TODO:Data����莟��v�f�̒ǉ�
    public CharacterData character;

    public string aiExpression;
    public EmotionType aiEmotion;
    public string aiMessage;
    public List<PlayerOption> playerOptions;

    public CharacterData Character => character;
    public string AiExpression => aiExpression;
    public EmotionType AiEmotion => aiEmotion;
    public string AiMessage => aiMessage;
    public List<PlayerOption> PlayerOptions => playerOptions;
}
