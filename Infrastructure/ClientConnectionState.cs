using System;

namespace Infrastructure;
public enum ClientConnectionState
{
    Alive,
    Connecting,
    Disconnected
}

public static class ClientConnection
{
    public delegate void ClientConnectionHandler(ClientConnectionEventArgs args);
    public static event ClientConnectionHandler? NetworkStatusChange;

    public static void OnNetworkStatusChange(ClientConnectionEventArgs args)
    {
        NetworkStatusChange?.Invoke(args);
    }
}

public class ClientConnectionEventArgs : EventArgs
{
    public ClientConnectionState State;
}
