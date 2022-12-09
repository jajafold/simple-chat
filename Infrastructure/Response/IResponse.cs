using Infrastructure.Messages;

namespace Infrastructure.Response;

public interface IResponse<TMessageType> where TMessageType : Message
{
}