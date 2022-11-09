using System.Windows.Forms;
using Chat.Infrastructure;

namespace Chat.UI
{
    public class Chat : IWritable
    {
        private readonly RichTextBox _source;

        public Chat(RichTextBox source)
        {
            _source = source;
        }

        public void Write(string text)
        {
            _source.Text += text + '\n';
        }
    }
}