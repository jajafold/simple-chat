using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Response
{
    [Serializable]
    public class UsersOnlineResponse : IResponse<TextMessage>
    {
        public TextMessage Message { get; set; }
        public IEnumerable<string> UserNames { get; set; }

        public UsersOnlineResponse(TextMessage message, IEnumerable<string> userNames)
        {
            Message = message;
            UserNames = userNames;
        }
        public UsersOnlineResponse(){}
    }
}