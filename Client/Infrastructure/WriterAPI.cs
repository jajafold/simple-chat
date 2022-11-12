namespace Infrastructure
{
    public class Writer : IWriter
    {
        private readonly IWritable _output;
        
        public Writer(IWritable output)
        {
            _output = output;
        }
        public void Write(string text)
        {
            _output.Write(text);
        }
    }
}
