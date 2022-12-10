using System;

namespace Infrastructure.Exceptions;

public class PasswordRequiredException : Exception
{
    public PasswordRequiredException(string? message) : base(message)
    {
    }

    public PasswordRequiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}