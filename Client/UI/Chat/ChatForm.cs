using System;
using System.Drawing;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure;
using Infrastructure.Updater;

namespace Chat.UI.Chat
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        public ChatForm(Client client)
        {
            InitializeComponent();
            _client = client;
            _client.SetTo<Writer>(new GlobalChatWriter(new UI.Chat.Chat(chatWindow)));
            _client.SetTo(new Updater<string>(new OnlineUsers(ActiveUsers)));
            
            chatWindow.TextChanged += ChatWindowChangedHandler;
            chatWindow.Disposed += ChatWindowClosedHandler;
            ClientConnection.NetworkStatusChange += ChangeNetworkStatus;

            networkStatus.ForeColor = Color.Gold;
            networkStatus.Text = @"Подключение...";
        }

        // private void OnConnectionException()
        // {
        //     TopMost = false;
        //     if (MessageBox.Show(
        //             "Возникли проблемы с соединением. Проверьте свое подключение к интернету и попробуйте еще раз",
        //             "Ошибка соединения",
        //             MessageBoxButtons.RetryCancel,
        //             MessageBoxIcon.Error,
        //             MessageBoxDefaultButton.Button1,
        //             MessageBoxOptions.DefaultDesktopOnly
        //         ) == DialogResult.Retry)
        //     {
        //         if (_client.CurrentRoom != null) 
        //             _client.Join(_client.CurrentRoom.Value, null);
        //         TopMost = true;
        //     }
        //     else
        //         Environment.Exit(0);
        // }

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
