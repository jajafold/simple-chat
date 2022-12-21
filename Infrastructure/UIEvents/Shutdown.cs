namespace Infrastructure.UIEvents;

public static class Shutdown
{
    public delegate void ClientShutdownHandler();
    public static event ClientShutdownHandler? ClientShutdown;
    public static void OnClientShutdown()
    {
        ClientShutdown?.Invoke();
    }
}