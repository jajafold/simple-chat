using System;

namespace Infrastructure.Messages
{
    [Serializable]
    public abstract class Message : IMessage
    {
        public DateTime SendTime { get; set; }
        public Guid ChatRoom { get; }
        public long TimeStamp { get; }
        public string Name { get; set; }
        public string Text { get; set; }

        public Message(DateTime sendTime, Guid chatRoom, string name)
        {
            SendTime = sendTime;
            ChatRoom = chatRoom;
            TimeStamp = sendTime.Ticks;
            Name = name;
        }

        public abstract string ToFlatString();
    }
}