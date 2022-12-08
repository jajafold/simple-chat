using System;
using System.Windows.Forms;
using Chat.Domain;
using Chat.UI.Chat;
using Infrastructure.Models;
using Infrastructure.Updater;

namespace Chat.UI.RoomSelection
{
    public partial class RoomSelection : Form
    {
        private readonly string _login;
        private readonly Client _client;

        public RoomSelection(string username)
        {
            InitializeComponent();
            _login = username;
            _client = new Client("http://localhost:5034", username);
            _client.SetTo(
                new Updater<RoomViewModel>(new RoomsTable(_roomSelectionTable)));

            _roomSelectionTable.MultiSelect = false;
            _roomSelectionTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _buttonConnect.Enabled = false;
            
            _roomSelectionTable.CellClick += 
                (sender, args) => {
                    if (args.RowIndex == -1) return;

                    _buttonConnect.Enabled = true;
                };

            _buttonConnect.Click +=
                (sender, args) =>
                {
                    var selected = _roomSelectionTable.SelectedRows[0];
                    _DEBUG_selected.Text = 
                        $"{selected.Cells[0].Value}\n{selected.Cells[3].Value}";
                    
                    var chatID = selected.Cells[3].Value.ToString();
                    var 
                };

            _buttonRoomCreation.Click += (sender, args) =>
            {
                var roomCreation = new RoomCreation(_client, this);
                roomCreation.ShowDialog(this);
            };

            Closing += (sender, args) => { Environment.Exit(0); };
        }
    }
}
