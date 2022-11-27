using System;
using Chat.Domain;
using Infrastructure;
using System.Windows.Forms;
using Ninject;
using Ninject.Infrastructure.Language;

namespace Chat.UI
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        
        public ChatForm(string name)
        {
            InitializeComponent();

            _client = new Client("http://localhost:5034", 
                name, 
                new GlobalChatWriter(new Chat(chatWindow)),
                new Writer(new OnlineUsers(ActiveUsers)));
            
            _client.Join(Guid.NewGuid());
            chatWindow.TextChanged += ChatWindowChangedHandler;
            chatWindow.Disposed += ChatWindowClosedHandler;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var msg = inputMessageField.Text;
            _client.Send(msg);
            inputMessageField.Clear();
            inputMessageField.Focus();
        }

        private void ChatWindowChangedHandler(object sender, EventArgs e)
        {
            chatWindow.SelectionStart = chatWindow.Text.Length;
            chatWindow.ScrollToCaret();
        }

        private void ChatWindowClosedHandler(object sender, EventArgs e) => Environment.Exit(0);

        private void chatWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
