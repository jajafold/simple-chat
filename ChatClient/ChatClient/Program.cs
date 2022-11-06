using System;

namespace ChatClient
{
    class Program
    {
        private static void Main()
        {
            var cl = new Client("127.0.0.1", 8888, new ConsoleWriter(), "TEST");
            cl.Connect();
        }
    }

    class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}