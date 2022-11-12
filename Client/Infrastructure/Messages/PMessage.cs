namespace Chat.Infrastructure.Messages
{
    public interface IPersonalMessage
    {
        public string ReceiverId { get; set; }
    }
}