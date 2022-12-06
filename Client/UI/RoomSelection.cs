using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat.UI
{
    public partial class RoomSelection : Form
    {
        private readonly string _login;
        private ChatForm _chat;

        public RoomSelection(string username)
        {
            InitializeComponent();
            _login = username;
            _roomSelectionTable.Rows.Add("Room 1", "Для всех", "12 / 50");
            _roomSelectionTable.Rows.Add("Room 2", "Для всех", "12 / 50");
            _roomSelectionTable.Rows.Add("Room 3", "Для всех", "12 / 50");
            _roomSelectionTable.Rows.Add("Room 4", "Для всех", "12 / 50");
            _roomSelectionTable.MultiSelect = false;

            _buttonConnect.Enabled = false;
            _roomSelectionTable.CellClick += 
                (sender, args) => {
                    if (args.RowIndex == -1) return;

                    _buttonConnect.Enabled = true;
                    _roomSelectionTable.Rows[args.RowIndex].Selected = true;
                };

            _buttonConnect.Click +=
                (sender, args) => {
                    _DEBUG_selected.Text = _roomSelectionTable.SelectedRows[0].Cells[0].Value.ToString();
                    _chat = new ChatForm(_login);
                    _chat.Show(this);
                    Hide();
                };

        } 
        
    }
}
