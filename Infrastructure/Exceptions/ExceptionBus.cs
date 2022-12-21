using System;
using System.Collections.Generic;

namespace Infrastructure.Exceptions;

public class ExceptionBus
{
    private readonly Queue<Exception> _bus = new();
    public bool IsProcessing { get; private set; }
    
    public ExceptionBus(){}

    public void Register(Exception e)
    {
        _bus.Enqueue(e);
    }

    public Exception Handle()
    {
        IsProcessing = true;
        return _bus.Dequeue();
    }
}