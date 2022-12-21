#pragma warning disable CA1416

#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.UIEvents;
using Infrastructure.Worker;

namespace Chat.Domain;

public class Client
{ 
    public string Name { get; }
    public Guid? CurrentRoom => _networkClient.CurrentRoom;

    private readonly NetworkClient _networkClient;

    private readonly CancelableWorker _updateMessages;
    private readonly CancelableWorker _updateUsers;
    private readonly CancelableWorker _updateRooms;
    private readonly CancelableWorker _updatePing;

    private bool _shutdown; 

    public Client(string host, string name)
    {
        Name = name;

        _networkClient = new NetworkClient(host, name);
        _updateMessages = new CancelableWorker(GetNewMessages, 200);
        _updateUsers = new CancelableWorker(UpdateOnlineUsers, 300);
        _updateRooms = new CancelableWorker(UpdateRooms, 400);
        _updatePing = new CancelableWorker(Ping, 1000);
        
        _updateRooms.Start();
        _updatePing.Start();
    }

    public void Shutdown()
    {
        _updatePing.Cancel();
        _updateMessages.Cancel();
        _updateRooms.Cancel();
        _updateUsers.Cancel();
        _shutdown = true;
    }

    private async void Ping()
    {
        var ping = await _networkClient.Ping();
        ClientConnection.OnNetworkStatusChange(new ClientConnectionEventArgs
        {
            Ping = ping,
            State = ClientConnectionState.Alive
        });
    }

    public async void Join(Guid chatRoomId)
    {
        var confirmation = await _networkClient.TryJoin(chatRoomId);
        if (!confirmation.CanFit)
            throw new RoomIsFullException($"Room {chatRoomId} is full");
        
        if (confirmation.NeedsConfirmation)
            throw new PasswordRequiredException($"Password required for room {chatRoomId}");

        AcceptJoining(chatRoomId);
    }

    public async void Validate(Guid chatRoomId, string? password)
    {
        var validation = await _networkClient.ValidatePassword(chatRoomId, password);
        if (!validation.Success)
            throw new IncorrectPasswordException($"Incorrect password \"{password}\" for room {chatRoomId}");
        
        AcceptJoining(chatRoomId);
    }

    private void AcceptJoining(Guid chatRoomId)
    {
        RoomJoining.OnUserJoinedRoom(new UserJoinedRoomEventArgs());
        _updateRooms.Cancel();
        _updateMessages.Start();
        _updateUsers.Start();
    }
    
    public async void Leave()
    {
        _updateUsers.Cancel();
        _updateMessages.Cancel();

        await _networkClient.Leave();
        _updateRooms.Start();
    }

    public async void Send(string message) => await _networkClient.Send(message);
    public async Task<Guid?> CreateRoom(string roomName, string? password, int capacity) =>
        await _networkClient.CreateRoom(roomName, password, capacity);
    
    private async void GetNewMessages()
    {
        var messages = await _networkClient.GetNewMessages();
        if (messages is null || messages.Length == 0) return;
            
        ChatEvents.OnChatMessagesChange(new ChatMessagesChangeEventArgs {Messages = messages});
    }

    private async void UpdateOnlineUsers()
    {
        var users = await _networkClient.UpdateOnlineUsers();
        if (users is null) return;
        
        ChatEvents.OnChatUsersChange(new ChatUsersChangeEventArgs {Users = users});
    }

    private async void UpdateRooms()
    {
        var rooms = await _networkClient.UpdateRooms();
        if (rooms is null) return;
        
        RoomsEvents.OnRoomsTableChange(new RoomsTableChangeEventArgs {Rooms = rooms});
    }
}