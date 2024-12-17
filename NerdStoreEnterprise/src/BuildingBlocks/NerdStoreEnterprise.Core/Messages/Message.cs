namespace NerdStoreEnterprise.Core.Messages;

public class Message
{
    public Message()
    {
        MessageType = GetType().Name;
    }

    public string MessageType { get; protected set; } = string.Empty;
    public Guid AggregateId { get; protected set; }
}