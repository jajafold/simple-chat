using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.UIEvents;
using Infrastructure.Updater;

namespace Chat.UI.Chat
{
    public partial class ChatForm : Form
    {
        private readonly Client _client;
        private readonly Writer _chatWriter;
        private readonly Updater<string> _userUpdater;
        public ChatForm(Client client)
        {
            InitializeComponent();
            _client = client;
            _chatWriter = new GlobalChatWriter(new Chat(chatWindow));
            _userUpdater = new Updater<string>(new OnlineUsers(ActiveUsers));
            
            chatWindow.TextChanged += ChatWindowChangedHandler;
            chatWindow.Disposed += ChatWindowClosedHandler;
            
            ClientConnection.NetworkStatusChange += UpdateNetworkStatusUI;
            ChatEvents.ChatMessagesChange += UpdateMessages;
            ChatEvents.ChatUsersChange += UpdateUsers;

            networkStatus.ForeColor = Color.Gold;
            networkStatus.Text = @"Подключение...";
        }

        private void UpdateMessages(ChatMessagesChangeEventArgs args)
        {
            foreach (var message in args.Messages)
                _chatWriter.Write(message.Content.ToFlatString());
        }

        private void UpdateUsers(ChatUsersChangeEventArgs args)
        {
            _userUpdater.Update(args.Users);
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
            ClientConnection.NetworkStatusChange -= UpdateNetworkStatusUI;
            ChatEvents.ChatMessagesChange -= UpdateMessages;
            ChatEvents.ChatUsersChange -= UpdateUsers;
            _client.Leave();
            
            //Environment.Exit(0);
            ChatEvents.OnChatWindowClosed();
        }

        private void UpdateNetworkStatusUI(ClientConnectionEventArgs e) => BeginInvoke(ChangeNetworkStatus, e);
        private void UpdateMessagesUI(ChatMessagesChangeEventArgs e) => BeginInvoke(UpdateMessages);
        private void UpdateUsersUI(ChatUsersChangeEventArgs e) => BeginInvoke(UpdateUsers);

        private void ChangeNetworkStatus(ClientConnectionEventArgs e)
        {
            var status = e.State;
            switch (status)
            {
                case ClientConnectionState.Alive:
                    networkStatus.ForeColor = Color.Green;
                    networkStatus.Text = $@"Подключен [{e.Ping} ms]";
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
