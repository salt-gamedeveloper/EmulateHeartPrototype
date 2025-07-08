public class MessageData
{
    private MessageType _messageType;
    private string _message;

    public MessageType MessageType => _messageType;
    public string Message => _message;

    public MessageData(MessageType messageType, string message)
    {
        _messageType = messageType;
        _message = message;
    }
}
