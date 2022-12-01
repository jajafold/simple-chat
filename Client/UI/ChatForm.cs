using System;
using Infrastructure;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure.Updater;

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
                new Updater<string>(new OnlineUsers(ActiveUsers)));
            try
            {
                _client.Join(Guid.NewGuid());
            }
            catch (Exception e)
            {
                Console.WriteLine("Сервер недоступен");
                throw;
            }
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

        private void ChatWindowClosedHandler(object sender, EventArgs e)
        {
            _client.Leave();
            Environment.Exit(0);
        }

        private void chatWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
