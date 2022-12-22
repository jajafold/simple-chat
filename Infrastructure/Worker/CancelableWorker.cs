using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Worker;

public class CancelableWorker
{
    private readonly int _delay;
    private readonly Action _work;
    private Thread? _workThread;
    private bool _cancellationToken;

    public CancelableWorker(Action work, int msDelay)
    {
        _work = work;
        _delay = msDelay;
    }

    public void Start()
    {
        _cancellationToken = false;
        _workThread = new Thread(DoWork);
        
        _workThread.Start();
    }

    public void Cancel()
    {
        _cancellationToken = true;
    }

    private void DoWork()
    {
        try
        {
            while (!_cancellationToken) { 
                Thread.Sleep(_delay);
                _work.Invoke();
            }
        }
        catch (Exception e)
        {
            _cancellationToken = true;
            throw;
        }
    }
}