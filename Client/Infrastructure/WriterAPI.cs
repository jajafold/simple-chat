namespace Chat.Infrastructure
{
    public class Writer<TOutput> : IWriter
        where TOutput : IWritable
    {
        private readonly TOutput _output;
        public Writer(TOutput output)
        {
            _output = output;
        }
        public void WriteLine(string text)
        {
            _output.Write(text);
        }
    }
}
