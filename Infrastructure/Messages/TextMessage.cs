using System;

namespace Infrastructure.Messages;

[Serializable]
public class TextMessage : Message
{
    public TextMessage(string text, Guid chatRoom, DateTime sendTime, string name) : base(sendTime, chatRoom, name)
    {
        Text = text;
    }


    public override string ToFlatString()
    {
        return $"[{SendTime.ToShortTimeString()}] {Name}: {Text}";
    }
}