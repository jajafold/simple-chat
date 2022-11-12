using System;

namespace ChatServer
{
    public abstract class PMessage : Message
    {
        public string ReceiverId { get; set; }
        public PMessage(DateTime sendTime, string senderId, string name, string receiverId) : base(sendTime, senderId, name)
        {
            ReceiverId = receiverId;
        }

        public abstract override string ToFlatString();
    }
}