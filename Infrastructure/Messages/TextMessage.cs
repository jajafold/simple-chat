using System;

namespace Infrastructure.Messages
{
    [Serializable]
    public class TextMessage : Message
    {
        public TextMessage(string text, Guid chatroom, DateTime sendTime, string name) : base(sendTime, chatroom, name)
        {
            Text = text;
        }

        public override string ToFlatString()
        {
            return $"[{SendTime.ToShortTimeString()}] {Name}: {Text}";
        }
    }
}