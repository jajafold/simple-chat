using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions;

public class Retry
{
    public sealed class Executor
    {
        public Exception? Exception { get; private set; }
        public bool FinishedSuccessfully { get; private set; }

        public async Task<TResult?> Execute<TResult>(
            Func<Task<TResult?>> func,
            int msInterval = 50,
            int retryCount = 5
        )
        {
            TResult? result = default;
            FinishedSuccessfully = false;

            for (var iteration = 0; iteration < retryCount; iteration++)
            {
                try
                {
                    result = await func.Invoke();
                    
                    ClientConnection.OnNetworkStatusChange(new ClientConnectionEventArgs {State = ClientConnectionState.Alive});
                    FinishedSuccessfully = true;
                    
                    return result;
                }
                catch (Exception exception)
                {
                    Exception = exception;
                    ClientConnection.OnNetworkStatusChange(new ClientConnectionEventArgs {State = ClientConnectionState.Connecting});
                }

                Thread.Sleep(msInterval);
            }
            
            ClientConnection.OnNetworkStatusChange(new ClientConnectionEventArgs {State = ClientConnectionState.Disconnected});
            FinishedSuccessfully = false;
            
            return result;
        }
    }
    
}