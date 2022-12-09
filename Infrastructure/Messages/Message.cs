using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Messages;

[Serializable]
[Keyless]
[Table("Messages")]
public class Message : IMessage
{
    public Message(DateTime sendTime, Guid chatRoom, string name)
    {
        SendTime = sendTime;
        ChatRoom = chatRoom;
        TimeStamp = sendTime.Ticks;
        Name = name;
    }

    public DateTime SendTime { get; set; }
    public Guid ChatRoom { get; set; }
    public long TimeStamp { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }

    public virtual string ToFlatString()
    {
        return "";
    }
}