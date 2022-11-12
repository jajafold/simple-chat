using System;

namespace ChatServer.Response
{
    public interface IResponse<TMessageType> where TMessageType : Message
    {
    }
}