using System;

namespace Infrastructure.Exceptions;

public class JoinChatRoomException : ConnectionException
{
    public JoinChatRoomException(string? message) : base(message) { }
    public JoinChatRoomException(string? message, Exception? innerException) : base(message, innerException) { }
}