using System;
using System.Threading;
using System.Windows.Forms;
using ChatClient;

namespace chat
{
    public partial class AuthorizeForm : Form
    {
        public AuthorizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = richTextBox1.Text;
            var client = new Client("127.0.0.1", 8888, name);
            var tcpThread = new Thread(() =>
            {
                client.Connect();
            });
            tcpThread.Start();

            var form = new ChatForm(client);
            form.Visible = true;

            this.Visible = false;

        }
    }
}
