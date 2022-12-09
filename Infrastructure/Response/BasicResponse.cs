using System;
using Infrastructure.Messages;

namespace Infrastructure.Response;

[Serializable]
public class BasicResponse : IResponse<TextMessage>
{
    public BasicResponse(TextMessage message)
    {
        Message = message;
    }

    public BasicResponse()
    {
    }

    public TextMessage Message { get; set; }
}