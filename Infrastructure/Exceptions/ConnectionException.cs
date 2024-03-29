﻿using System;

namespace Infrastructure.Exceptions;

public class ConnectionException : Exception
{
    protected ConnectionException()
    {
    }

    public ConnectionException(string? message) : base(message)
    {
    }

    public ConnectionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}