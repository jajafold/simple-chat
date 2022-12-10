#pragma warning disable CA1416

#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

    public Client(string host, string name)
    {
        Name = name;

        _networkClient = new NetworkClient(host, name);
        _updateMessages = new CancelableWorker(GetNewMessages, 200);
        _updateUsers = new CancelableWorker(UpdateOnlineUsers, 200);
        _updateRooms = new CancelableWorker(UpdateRooms, 200);
        
        _updateRooms.Start();
    }

    public async void Join(Guid chatRoomId)
    {
        var confirmation = await _networkClient.TryJoin(chatRoomId);
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
    public async Task<Guid> CreateRoom(string roomName, string? password, int capacity) =>
        await _networkClient.CreateRoom(roomName, password, capacity);
    
    private async void GetNewMessages()
    {
        var messages = await _networkClient.GetNewMessages();
        if (messages.Length == 0) return;
            
        ChatEvents.OnChatMessagesChange(new ChatMessagesChangeEventArgs {Messages = messages});
    }

    private async void UpdateOnlineUsers()
    {
        var users = await _networkClient.UpdateOnlineUsers();
        ChatEvents.OnChatUsersChange(new ChatUsersChangeEventArgs {Users = users});
    }

    private async void UpdateRooms()
    {
        var rooms = await _networkClient.UpdateRooms();
        RoomsEvents.OnRoomsTableChange(new RoomsTableChangeEventArgs {Rooms = rooms});
    }
}