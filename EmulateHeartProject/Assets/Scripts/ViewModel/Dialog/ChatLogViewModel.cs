using System.Collections.Generic;

public class ChatLogViewModel
{
    private List<ChatLogItemViewModel> chatLogItems;
    private string titile;
    public List<ChatLogItemViewModel> ChatLogItems => chatLogItems;
    public string Titile => titile;

    /*
    public ChatLogViewModel(SingleStoryData singleStory)
    {
        chatLogItems = new List<ChatLogItemViewModel>();

        var first = singleStory.SingleStory.FirstOrDefault();
        TimeOfDay storyTimeOfDay = Enums.ParseTimeOfDay(singleStory.TimeOfDay);
        foreach (ScreenStoryData screenStoryData in singleStory.SingleStory)
        {
            chatLogItems.Add(new ChatLogItemViewModel(screenStoryData));
        }
        titile = $"Day{singleStory.Day}Åb{Enums.ConvertTime(storyTimeOfDay)}Åb{singleStory.Subtitle}";
    }
    */

    public ChatLogViewModel(SceneChatHistory sceneChatHistory, string title)
    {
        chatLogItems = new List<ChatLogItemViewModel>();
        foreach (MessageData messageData in sceneChatHistory.SceneMessages)
        {
            chatLogItems.Add(new ChatLogItemViewModel(messageData));
        }
        this.titile = title;
    }
}
