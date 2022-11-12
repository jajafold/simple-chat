using System;
using Infrastructure.Messages;

namespace Infrastructure.Response
{
    [Serializable]
    public class BasicResponse : IResponse<TextMessage>
    {
        public TextMessage Message { get; set; }

        public BasicResponse(TextMessage message)
        {
            Message = message;
        }
        public BasicResponse() {}
    }
}