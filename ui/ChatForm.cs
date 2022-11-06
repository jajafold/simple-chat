using System;
using ChatClient;
using System.Windows.Forms;

namespace chat
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        public ChatForm(Client client)
        {
            InitializeComponent();
            var writer = new ChatWriter(messages);
            _client = client;
            _client._writer = writer;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {

        }
        
    }
}
