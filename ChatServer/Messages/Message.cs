using System;

namespace ChatServer
{
    public abstract class Message
    {
        public DateTime SendTime { get; }
        public string SenderSenderId { get; }
        public string Name { get; }

        public Message(DateTime time, string senderId, string name)
        {
            SendTime = time;
            SenderSenderId = senderId;
            Name = name;
        }

        public abstract string ToFlatString();
    }
}