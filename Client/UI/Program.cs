using System;
using System.Net.Mime;
using System.Threading;
using System.Windows.Forms;
using Chat.UI.Authorization;
using Chat.UI.RoomSelection;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.UIEvents;

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
            // Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += Application_ThreadException;
            
            Application.Run(new AuthorizeForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            switch (e.Exception)
            {
                case PasswordRequiredException:
                    RoomJoining.OnTryJoinProtectedRoom();
                    break;
                case IncorrectPasswordException:
                    RoomJoining.OnIncorrectPasswordEntered();
                    break;
            }
            
            //MessageBox.Show($"In Program.cs :{e.Exception.Message}");
        }
    }
}
