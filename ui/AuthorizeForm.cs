using System;
using System.Threading;
using System.Windows.Forms;
using ChatClient;

namespace chat
{
    public partial class AuthorizeForm : Form
    {
        private ChatForm _chatForm;
        public AuthorizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = richTextBox1.Text;

            _chatForm = new ChatForm(name);
            _chatForm.Visible = true;
            this.Visible = false;
        }
    }
}
