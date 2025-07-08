public class ChatLogItemViewModel
{
    private MessageType messageType;
    private string message;
    private int messageRowCount;

    public MessageType MessageType => messageType;
    public string Message => message;
    public int MessageRowCount => messageRowCount;

    public ChatLogItemViewModel(MessageType messageType, string message)
    {
        this.messageType = messageType;
        this.message = message;
        messageRowCount = RowCount(message);
    }

    /*
    public ChatLogItemViewModel(ScreenStoryData screenStoryData)
    {
        messageType = Enums.ParseMessageType(screenStoryData.MessageType);
        message = screenStoryData.Message;
        messageRowCount = RowCount(message);
    }
    */

    public ChatLogItemViewModel(MessageData messageData)
    {
        messageType = messageData.MessageType;
        message = messageData.Message;
        messageRowCount = RowCount(message);
    }


    private int RowCount(string message)
    {
        int maxCharsPerLine = 20;
        string[] logicalLines = message.Split('\n');
        int totalLines = 0;

        foreach (string line in logicalLines)
        {
            int length = line.Length;

            // 1�s������ő� maxCharsPerLine �����ŉ��s����Ƃ��āA�K�v�ȍs�����v�Z
            int lineCount = (length + maxCharsPerLine - 1) / maxCharsPerLine;
            totalLines += lineCount;
        }

        return totalLines;
    }
}
