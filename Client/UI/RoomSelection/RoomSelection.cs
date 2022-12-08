using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Domain;
using Chat.UI.Chat;
using Infrastructure.Models;
using Infrastructure.Updater;
using Infrastructure.Exceptions;
using Infrastructure.UIEvents;

namespace Chat.UI.RoomSelection
{
    public partial class RoomSelection : Form
    {
        private readonly string _login;
        private readonly Client _client;
        
        private readonly Updater<RoomViewModel> _roomTableUpdater;

        private Guid _selectedRoomId;

        public RoomSelection(string username)
        {
            InitializeComponent();
            Application.ThreadException +=
                (sender, args) =>
                {
                    if (args.Exception is PasswordRequiredException)
                    {
                        var securityForm = new PasswordRequired(_client, _selectedRoomId);
                        securityForm.ShowDialog(this);
                    }
                    else
                    {
                        MessageBox.Show(args.Exception.ToString());
                    }
                };
            
            _login = username;
            _client = new Client("http://localhost:5034", username);
            _roomTableUpdater = new Updater<RoomViewModel>(new RoomsTable(_roomSelectionTable));
            
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
                    
                    _selectedRoomId = Guid.Parse(selected.Cells[3].Value.ToString());
                    
                    _client.Join(_selectedRoomId);
                    
                    var chat = new ChatForm(_client);
                    chat.Show();
                    
                    Hide();
                };

            _buttonRoomCreation.Click += (sender, args) =>
            {
                var roomCreation = new RoomCreation(_client, this);
                roomCreation.ShowDialog(this);
            };

            Closing += (sender, args) => { Environment.Exit(0); };

            RoomsEvents.RoomsTableChange += args =>
            {
                BeginInvoke(Update1, args);
            };
        }
        private void Update1(RoomsTableChangeEventArgs e)
        {
            _roomTableUpdater.Update(e.Rooms);
        }
        
    }
}
