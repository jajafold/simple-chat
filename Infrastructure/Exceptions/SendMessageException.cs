using System;

namespace Infrastructure.Exceptions;

public class SendMessageException : ConnectionException
{
    public SendMessageException(string? message) : base(message) { }
    public SendMessageException(string? message, Exception? innerException) : base(message, innerException) { }
}