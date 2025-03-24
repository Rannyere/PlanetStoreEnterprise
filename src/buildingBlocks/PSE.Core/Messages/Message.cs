using System;
namespace PSE.Core.Messages;

public abstract class Message
{
    public string MessageType { get; protected set; }

    public Guid AggregatedId { get; set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}