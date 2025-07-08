
public class GeminiRequestBuilder
{
    private ChatPromptBuilder chatPromptBuilder;

    public GeminiRequestBuilder()
    {
        chatPromptBuilder = new ChatPromptBuilder();
    }

    public string BuildChatStartRequest()
    {
        return chatPromptBuilder.BuildChatStartPrompt();
    }


    public string BuildChatRequest(string question)
    {
        return chatPromptBuilder.BuildChatPrompt(question);
    }

    public string BuildChatRechooseOptionsRequest(string previous)
    {
        return chatPromptBuilder.BuildRechooseOptionsPrompt(previous);
    }

    public string BuildChatEndRequest(string question)
    {
        return chatPromptBuilder.BuildChatEndPrompt(question);
    }
}
