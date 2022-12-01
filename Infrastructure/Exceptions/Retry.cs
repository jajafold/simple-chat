using System;
using System.Threading;

namespace Infrastructure.Exceptions;

public class Retry
{
    public static TResult? Execute<TResult>(
        Func<TResult> func,
        ClientConnectionCandler connectionEventHandler,
        out Exception? e,
        int msInterval = 200,
        int retryCount = 10
    ) where TResult : class
    {
        TResult? result = null;
        e = null;

        for (var iteration = 0; iteration < retryCount; iteration++)
        {
            try
            {
                result = func();
                connectionEventHandler.Invoke(new ClientConnectionEventArgs { State = ClientConnectionState.Alive});
                break;
            }
            catch (Exception exception)
            {
                e = exception;
                connectionEventHandler.Invoke(new ClientConnectionEventArgs { State = ClientConnectionState.Connecting});
            }

            Thread.Sleep(msInterval);
        }
        
        return result;
    }
}