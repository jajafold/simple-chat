using System;
using System.Threading;
using System.Windows.Forms;
using Chat.UI.Authorization;
using Infrastructure;
using Infrastructure.Exceptions;

namespace Chat.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthorizeForm());
        }
    }
}
