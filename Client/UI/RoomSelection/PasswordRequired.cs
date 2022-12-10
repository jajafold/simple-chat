using System;
using System.Windows.Forms;
using Chat.Domain;
using Chat.UI.Chat;
using Infrastructure.Exceptions;
using Infrastructure.UIEvents;

namespace Chat.UI.RoomSelection
{
    public partial class PasswordRequired : Form
    {
        private readonly Client _client;
        private readonly Guid _roomId;
        private readonly Form _parent;
        public PasswordRequired(Client client, Guid roomId, Form parent)
        {
            InitializeComponent();

            _client = client;
            _roomId = roomId;
            _parent = parent;
            
            _bConfirm.Click +=
                (sender, args) =>
                {
                    var password = _tbPassword.Text;
                    _client.Validate(_roomId, password);
                    
                    Close();
                };

            _bCancel.Click += (sender, args) => Close();
        }
    }
}
