
public class ConversationDataManager
{
    private SceneChatHistory sceneChatHistory;
    private string currentStoryId;

    //TODO: �Œ�X�g�[���[��Log�Ǘ�����

    public ConversationDataManager()
    {
        sceneChatHistory = new SceneChatHistory();
    }

    public void SceneHistoryReset()
    {
        sceneChatHistory = new SceneChatHistory();
    }

    public void AddSceneChat(MessageData messageData)
    {
        sceneChatHistory.AddMessage(messageData);
    }

    public SceneChatHistory GetSceneChatHistory()
    {
        return sceneChatHistory;
    }
}
