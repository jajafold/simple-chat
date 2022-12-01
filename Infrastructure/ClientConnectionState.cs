using System;

namespace Infrastructure;
public enum ClientConnectionState
{
    Alive,
    Connecting,
    Disconnected
}

public delegate void ClientConnectionCandler(ClientConnectionEventArgs args);

public class ClientConnectionEventArgs : EventArgs
{
    public ClientConnectionState State;
}
