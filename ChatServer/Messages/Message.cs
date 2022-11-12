using System;

namespace ChatServer
{
    [Serializable]
    public abstract class Message
    {
        public DateTime SendTime { get; set; }
        public string SenderId { get; set; }
        public string Name { get; set; }

        public Message(DateTime sendTime, string senderId, string name)
        {
            SendTime = sendTime;
            SenderId = senderId;
            Name = name;
        }
        
        public abstract string ToFlatString();
    }
}