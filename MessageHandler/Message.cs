namespace MessageHandler;

public class Message
{
    public Message(string title, string description, MessageTypes messageTypes)
    {
        Title = title;
        Description = description;
        MessageType = messageTypes;
    }

    public string Description { get; }
    public MessageTypes MessageType { get; }
    public string Title { get; }
}