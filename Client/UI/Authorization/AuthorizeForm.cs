﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace Chat.UI.Authorization
{
    // подумать что потом будет много разных форм ауторизы. Магический контракт про чат форм
    public partial class AuthorizeForm : Form
    {
        private RoomSelection.RoomSelection _roomSelection;

        public AuthorizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = richTextBox1.Text;

            _roomSelection = new RoomSelection.RoomSelection(username);
            _roomSelection.Show();
             Hide();
        }
    }
}
