using System.Threading;

namespace Infrastructure.Threads;

public class CancelableThread
{
    private readonly Thread _baseThread;
    
    public CancelableThread(ParameterizedThreadStart start)
    {
        _baseThread = new Thread(start);
    }

    public CancelableThread(ThreadStart start)
    {
        _baseThread = new Thread(start);
    }

    public void Start() => _baseThread.Start();
}