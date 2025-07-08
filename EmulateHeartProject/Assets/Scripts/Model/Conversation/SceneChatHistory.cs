using System.Collections.Generic;
using System.Linq;

public class SceneChatHistory
{
    private List<MessageData> sceneMessages;
    public List<MessageData> SceneMessages => sceneMessages;

    public SceneChatHistory()
    {
        sceneMessages = new List<MessageData>();
    }

    public void AddMessage(MessageData newMessage)
    {
        sceneMessages.Add(newMessage);
    }

    public string ToChatPromptString()
    {
        // 最新の10件のメッセージ、またはそれ未満のメッセージを取得します
        List<MessageData> recentMessages = sceneMessages
                                            .OrderByDescending(m => sceneMessages.IndexOf(m)) // 最新のメッセージが先頭になるように並べ替えます
                                            .Take(10) // 最大10件取得します
                                            .Reverse() // プロンプトでは古いものから新しいものへ順に表示されるように並べ替えます
                                            .ToList();

        // 各メッセージを「MessageType: Message」の形式で結合します
        return string.Join("\n", recentMessages.Select(m => $"{m.MessageType}: {m.Message}"));
    }
}
