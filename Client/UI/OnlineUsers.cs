using System.Windows.Forms;
using Infrastructure;

namespace Chat.UI
{
    public class OnlineUsers : IWritable
    {
        private readonly ListBox _source;

        public OnlineUsers(ListBox source)
        {
            _source = source;
        }

        public void Write(string text)
        {
            _source.Items.Add(text);
        }
    }
}