using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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
        private static readonly ExceptionBus ExceptionBus = new ExceptionBus();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
            
            
            Application.Run(new AuthorizeForm());
        }

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionBus.Register((Exception) e.ExceptionObject);
            Shutdown.OnClientShutdown();

            if (ExceptionBus.IsProcessing) return;
            var exception = ExceptionBus.Handle();

            switch (exception)
            {
                case ConnectionException:
                    OnConnectionException(exception);
                    break;
                default:
                    MessageBox.Show(exception.ToString());
                    break;
            }
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
                case RoomIsFullException:
                    RoomJoining.OnTryJoinFullRoom();
                    break;
                default:
                    MessageBox.Show(e.Exception.ToString());
                    break;
            }
        }

        private static void OnConnectionException(Exception e)
        {
            var dialog = MessageBox.Show(
                $"Возникли проблемы с соединением. Проверьте свое подключение к интернету и попробуйте еще раз\n\n\n" +
                $"---{e.Message}---",
                "Ошибка соединения",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(0);
        }
    }
}
