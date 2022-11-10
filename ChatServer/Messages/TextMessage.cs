using System;

namespace ChatServer
{
    [Serializable]
    public class TextMessage : Message
    {
        public string Text { get; }
        public TextMessage(string text, DateTime time, string senderId, string name) : base(time, senderId, name)
        {
            Text = text;
        }

        public override string ToFlatString()
        {
            return $"[{SendTime.Hour}:{SendTime.Minute}] {Name}: {Text}";
        }
    }
}