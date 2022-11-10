using System;

namespace ChatServer
{
    public abstract class PMessage : Message
    {
        public string ReceiverId { get; }
        public PMessage(DateTime time, string senderId, string name, string receiverId) : base(time, senderId, name)
        {
            ReceiverId = receiverId;
        }

        public abstract override string ToFlatString();
    }
}