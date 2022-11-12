using System;
using Chat.Domain;
using Infrastructure;
using System.Windows.Forms;
using Ninject;

namespace Chat.UI
{
    public partial class ChatForm : Form, ICanInject
    {
        private readonly Client _client;

        public void Inject()
        {
            DependencyInjector.Injector.Bind<IWritable>().To<Chat>().WithConstructorArgument(chatWindow);
            DependencyInjector.Injector.Bind<IWritable>().To<OnlineUsers>().WithConstructorArgument(ActiveUsers);
        }
        
        public ChatForm(string name)
        {
            InitializeComponent();
            Inject();
            
            _client = new Client("127.0.0.1", 8888, name);
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

        private void chatWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
