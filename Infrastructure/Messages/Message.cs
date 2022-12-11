using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Messages;

[Serializable]
public class Message : IMessage
{
    public Message(DateTime sendTime, Guid chatRoom, string name)
    {
        SendTime = sendTime;
        ChatRoom = chatRoom;
        TimeStamp = sendTime.Ticks;
        Name = name;
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id;
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