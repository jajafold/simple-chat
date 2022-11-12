using System;

namespace Chat.Infrastructure.Messages
{
    [Serializable]
    public class TextMessage : Message
    {
        public string Text { get; set; }
        public TextMessage(string text, DateTime sendTime, string senderId, string name) : base(sendTime, senderId, name)
        {
            Text = text;
        }

        public override string ToFlatString()
        {
            return $"[{SendTime.Hour}:{SendTime.Minute}] {Name}: {Text}";
        }
    }
}