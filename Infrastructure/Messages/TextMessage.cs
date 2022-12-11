using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messages;

[Serializable]
[Table("TextMessages")]
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