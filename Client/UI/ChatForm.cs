using System;
using System.Drawing;
using System.Threading;
using Infrastructure;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure.Exceptions;
using Infrastructure.Updater;

namespace Chat.UI
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        private readonly Guid _connectTo;
        
        public ChatForm(string name)
        {
            InitializeComponent();
            
            chatWindow.TextChanged += ChatWindowChangedHandler;
            chatWindow.Disposed += ChatWindowClosedHandler;
            Application.ThreadException += ThreadExceptionCaught;
            ClientConnection.NetworkStatusChange += ChangeNetworkStatus;

            _client = new Client("http://localhost:5034", 
                name, 
                new GlobalChatWriter(new Chat(chatWindow)),
                new Updater<string>(new OnlineUsers(ActiveUsers)));
            
            networkStatus.ForeColor = Color.Gold;
            networkStatus.Text = @"Подключение...";

            _connectTo = Guid.NewGuid();
            
            _client.Join(_connectTo);
        }

        private void OnConnectionException()
        {
            TopMost = false;
            if (MessageBox.Show(
                    "Возникли проблемы с соединением. Проверьте свое подключение к интернету и попробуйте еще раз",
                    "Ошибка соединения",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                ) == DialogResult.Retry)
            {
                _client.Join(_connectTo);
                TopMost = true;
            }
            else
                Environment.Exit(0);
        }

        private void OnUnhandledException(ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }

        private void ThreadExceptionCaught(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is ConnectionException)
                OnConnectionException();
            else 
                OnUnhandledException(e);
            
            Activate();
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

        private void ChangeNetworkStatus(ClientConnectionEventArgs e)
        {
            var status = e.State;
            switch (status)
            {
                case ClientConnectionState.Alive:
                    networkStatus.ForeColor = Color.Green;
                    networkStatus.Text = @"Подключен";
                    break;
                case ClientConnectionState.Connecting:
                    networkStatus.ForeColor = Color.Gold;
                    networkStatus.Text = @"Подключение...";
                    break;
                case ClientConnectionState.Disconnected:
                    networkStatus.ForeColor = Color.Red;
                    networkStatus.Text = @"Отключен";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e));
            }
        }
    }
}
