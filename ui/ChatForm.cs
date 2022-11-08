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

            _writer = new ChatWriter(messages);
            _client = new Client("127.0.0.1", 8888, _writer, name);
            _client.Connect();
            FormClosed += FormClosedEvent;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var msg = String.Text;
            _client.Send(msg);
            String.Clear();
            String.Focus();
        }  

        private void FormClosedEvent(object sender, EventArgs e)
        {
            _client.Disconnect();
        }
    }
}
