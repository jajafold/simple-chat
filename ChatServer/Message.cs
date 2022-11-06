using System;

namespace ChatServer
{
    public class Message
    {
        public DateTime SendTime { get; }
        public string SenderId { get; }
        public string Text { get; }

        public Message(string text, DateTime time, string id)
        {
            SendTime = time;
            SenderId = id;
            Text = text;
        }
    }
}