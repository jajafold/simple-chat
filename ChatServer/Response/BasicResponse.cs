using System;

namespace ChatServer.Response
{
    [Serializable]
    public class BasicResponse : IResponse<TextMessage>
    {
        public TextMessage Message { get; }

        public BasicResponse(TextMessage message)
        {
            Message = message;
        }
        public BasicResponse() {}
    }
}