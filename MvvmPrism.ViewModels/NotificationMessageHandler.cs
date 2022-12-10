using MessageHandler;

namespace MvvmPrism.ViewModels;

public static class NotificationMessageHandler
{
    static NotificationMessageHandler()
    {
        MessageHandler = new NotificationHandler();
    }

    public static NotificationHandler MessageHandler { get; }

    public static void AddError(string title, string description)
    {
        MessageHandler.Add(new Message(title, description, MessageTypes.Error));
    }

    public static void AddInfo(string title, string description)
    {
        MessageHandler.Add(new Message(title, description, MessageTypes.Information));
    }

    public static void AddSuccess(string title, string description)
    {
        MessageHandler.Add(new Message(title, description, MessageTypes.Success));
    }
}