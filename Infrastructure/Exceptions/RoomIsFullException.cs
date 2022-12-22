using System;

namespace Infrastructure.Exceptions;

public class RoomIsFullException : Exception
{
    public RoomIsFullException(string? message) : base(message)
    {
    }

    public RoomIsFullException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}