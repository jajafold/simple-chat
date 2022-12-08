using System;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure.Exceptions;

namespace Chat.UI.RoomSelection
{
    public partial class PasswordRequired : Form
    {
        private readonly Client _client;
        private readonly Guid _roomId;
        public PasswordRequired(Client client, Guid roomId)
        {
            InitializeComponent();

            Application.ThreadException +=
                (sender, args) =>
                {
                    if (args.Exception is IncorrectPasswordException)
                        MessageBox.Show("Неправильный пароль!");
                    else
                        MessageBox.Show(args.Exception.ToString());
                };
            
            _client = client;
            _roomId = roomId;
            
            _bConfirm.Click +=
                (sender, args) =>
                {
                    var password = _tbPassword.Text;
                    _client.Validate(_roomId, password);
                };
        }
    }
}
