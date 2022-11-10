using System;

namespace ChatServer
{
    [Serializable]
    public abstract class Message
    {
        public DateTime SendTime { get; }
        public string SenderId { get; }
        public string Name { get; }

        public Message(DateTime time, string senderId, string name)
        {
            SendTime = time;
            SenderId = senderId;
            Name = name;
        }
        
        public abstract string ToFlatString();
    }
}