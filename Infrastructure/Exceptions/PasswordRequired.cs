using System;

namespace Infrastructure.Exceptions;

public class PasswordRequired : Exception
{
    public PasswordRequired(string? message) : base(message) { }
    public PasswordRequired(string? message, Exception? innerException) : base(message, innerException) { }
}