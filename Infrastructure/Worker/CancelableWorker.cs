using System;
using System.Threading;

namespace Infrastructure.Worker;

public class CancelableWorker
{
    private readonly int _delay;
    private readonly Action _work;
    private readonly Thread _workThread;
    private bool _cancellationToken;

    public CancelableWorker(Action work, int msDelay)
    {
        _work = work;
        _delay = msDelay;
        _workThread = new Thread(DoWork);
    }

    public void Start()
    {
        _cancellationToken = false;
        _workThread.Start();
    }

    public void Cancel()
    {
        _cancellationToken = true;
    }

    private void DoWork()
    {
        while (!_cancellationToken)
        {
            Thread.Sleep(_delay);
            _work.Invoke();
        }
    }
}