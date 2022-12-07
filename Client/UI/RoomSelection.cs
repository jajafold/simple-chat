using System;
using System.Windows.Forms;
using Chat.Domain;
using Infrastructure.Models;
using Infrastructure.Updater;

namespace Chat.UI
{
    public partial class RoomSelection : Form
    {
        private readonly string _login;
        private readonly Client _client;
        private ChatForm _chat;

        public RoomSelection(string username)
        {
            InitializeComponent();
            _login = username;
            _client = new Client("http://localhost:5034", username);
            _client.SetTo<Updater<RoomViewModel>>(
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
                (sender, args) => {
                    _DEBUG_selected.Text = _roomSelectionTable.SelectedRows[0].Cells[0].Value.ToString()!;
                    // _chat = new ChatForm(_client);
                    // _chat.Show(this);
                    // Hide();
                };

            Closing += (sender, args) => { Environment.Exit(0); };
        }
    }
}
