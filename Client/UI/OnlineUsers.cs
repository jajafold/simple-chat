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
            if (_source.Items.Contains(text)) return;
            _source.Items.Add(text);
        }
    }
}