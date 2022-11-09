using System;

namespace ChatServer
{
    public class Message
    {
        public DateTime SendTime { get; }
        public string SenderId { get; }
        public string Name { get; }
        public string Text { get; }

        public Message(string text, DateTime time, string id, string name)
        {
            SendTime = time;
            SenderId = id;
            Text = text;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{SendTime.Hour}:{SendTime.Minute}] {Name}: {Text}";
        }
    }
}