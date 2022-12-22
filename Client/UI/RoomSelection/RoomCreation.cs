using Chat.Domain;
using System.Windows.Forms;
using Chat.UI.Chat;

namespace Chat.UI.RoomSelection
{
    public partial class RoomCreation : Form
    {
        private readonly Client _creator;
        private ChatForm _chat;
        private readonly Form _parent;
        public RoomCreation(Client client, Form parent)
        {
            InitializeComponent();

            _creator = client;
            _parent = parent;
            _tbRoomName.Text = $"{_creator.Name}'s room";

            _cbIsPasswordSet.CheckedChanged +=
                (sender, args) => _tbPassword.Enabled = _cbIsPasswordSet.Checked;

            _bCancel.Click +=
                (sender, args) => Close();

            _bCreate.Click += async (sender, args) =>
            {
                var password = _cbIsPasswordSet.Checked ? _tbPassword.Text : null;
                var roomId = await _creator.CreateRoom(_tbRoomName.Text, password, (int)_nudRoomCapacity.Value);
                if (!roomId.HasValue) return;
                
                _creator.Validate(roomId.Value, password);
                Close();
            };
        }
    }
}
