using System;
using ChatClient;
using System.Windows.Forms;

namespace chat
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        private readonly ChatWriter _writer;
        public ChatForm(string name)
        {
            InitializeComponent();

            _writer = new ChatWriter(chatWindow);
            _client = new Client("127.0.0.1", 8888, _writer, name);
            _client.Connect();
            FormClosed += FormClosedHandler;
            chatWindow.TextChanged += ChatWindowChangedHandler;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var msg = inputMessageField.Text;
            _client.Send(msg);
            inputMessageField.Clear();
            inputMessageField.Focus();
        }  

        private void FormClosedHandler(object sender, EventArgs e)
        {
            _client.Disconnect();
        }

        private void ChatWindowChangedHandler(object sender, EventArgs e)
        {
            chatWindow.SelectionStart = chatWindow.Text.Length;
            chatWindow.ScrollToCaret();
        }
    }
}
