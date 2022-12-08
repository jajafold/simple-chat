using System;

namespace Infrastructure.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException(string? message) : base(message) { }
    public IncorrectPasswordException(string? message, Exception? innerException) : base(message, innerException) { }
}