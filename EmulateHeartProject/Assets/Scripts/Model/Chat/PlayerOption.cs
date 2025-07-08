[System.Serializable]
public class PlayerOption
{
    public EmotionType optionEmotion;
    public string optionMessage;

    public EmotionType OptionEmotion => optionEmotion;
    public string OptionMessage => optionMessage;

    public PlayerOption(EmotionType emotionType = EmotionType.Normal, string message = "")
    {
        optionEmotion = emotionType;
        optionMessage = message;
    }
}
