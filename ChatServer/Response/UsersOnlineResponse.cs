using System;
using System.Collections.Generic;

namespace ChatServer.Response
{
    [Serializable]
    public class UsersOnlineResponse : IResponse<TextMessage>
    {
        public TextMessage Message { get; }
        public IEnumerable<string> UserNames { get; }

        public UsersOnlineResponse(TextMessage message, IEnumerable<string> userNames)
        {
            Message = message;
            UserNames = userNames;
        }
        public UsersOnlineResponse(){}
    }
}