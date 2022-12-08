using Infrastructure;

namespace Chat.UI.Chat
{
    public class GlobalChatWriter : Writer 
    {
        public GlobalChatWriter(IWritable output) : base(output) { }
    }
    
    public class PersonalChatWriter : Writer
    {
        public PersonalChatWriter(IWritable output) : base(output) { }
    }
}