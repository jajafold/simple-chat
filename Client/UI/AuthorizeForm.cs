using System;
using System.Windows.Forms;

namespace Chat.UI
{
    // подумать что потом будет много разных форм ауторизы. Магический контракт про чат форм
    public partial class AuthorizeForm : Form
    {
        private ChatForm _chatForm;
        public AuthorizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = richTextBox1.Text;

            _chatForm = new ChatForm(username);
            //_chatForm.Visible = true;
            //Visible = false;
            _chatForm.Show(this);
             Hide();
        }
    }
}
