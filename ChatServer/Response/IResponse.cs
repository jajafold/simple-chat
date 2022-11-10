using System;

namespace ChatServer.Response
{
    public interface IResponse<out TMessageType> where TMessageType : Message
    {
        public TMessageType Message { get; }
    }
}