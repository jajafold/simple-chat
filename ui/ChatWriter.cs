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
        private Label _messages;
        public ChatWriter(Label messages)
        {
            _messages = messages;
        }
        public void WriteLine(string text)
        {
            _messages.Text += text + '\n';
        }
    }
}
