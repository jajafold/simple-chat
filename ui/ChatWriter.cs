using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatClient;

namespace chat
{
    class ChatWriter : IWriter
    {
        private RichTextBox _messages;
        public ChatWriter(RichTextBox messages)
        {
            _messages = messages;
        }
        public void WriteLine(string text)
        {
            _messages.Text += text + '\n';
        }
    }
}
