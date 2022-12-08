using System;

namespace Infrastructure.Exceptions;

public class IncorrectPassword : Exception
{
    public IncorrectPassword(string? message) : base(message) { }
    public IncorrectPassword(string? message, Exception? innerException) : base(message, innerException) { }
}