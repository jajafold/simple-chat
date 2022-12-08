#pragma warning disable CA1416

#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using Infrastructure.Updater;

namespace Chat.Domain;

public class Client
{ 
    public string Name { get; }
    public Guid? CurrentRoom { get; private set; }

    private readonly NetworkClient _networkClient;
    private Writer _chatWriter;
    private Updater<string> _onlineUsersWriter;
    private Updater<RoomViewModel> _roomsUpdater;

    private readonly Thread _receiveUpdate;
    private readonly Thread _onlineUsersUpdate;
    private readonly Thread _roomsUpdate;
    
    private bool _cancellationTokenA;
    private bool _cancellationTokenB;

    public Client(string host, string name)
    {
        Name = name;

        _networkClient = new NetworkClient(host, name);
        _receiveUpdate = new Thread(GetNewMessages);
        _onlineUsersUpdate = new Thread(UpdateOnlineUsers);
        _roomsUpdate = new Thread(UpdateRooms);
        
        _roomsUpdate.Start();
    }

    public void SetTo<T>(T fieldValue)
    where T : class
    {
        var field = typeof(Client)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.FieldType == typeof(T));
         if (field is null) throw new ArgumentException("Cannot find this type of field");
         field.SetValue(this, fieldValue);
    }

    public async void Join(Guid chatRoomId, string? password)
    {
        _cancellationTokenB = false;
        var confirmation = await _networkClient.Join(chatRoomId, password);
        if (!confirmation.NeedsConfirmation)
            throw new PasswordRequired($"Password required for room {chatRoomId}");
        
        //TODO: Это все нужно вынести в Validate, о шибки ловить в форме
        CurrentRoom = chatRoomId;
        _cancellationTokenA = false;
        _receiveUpdate.Start();
        _onlineUsersUpdate.Start();
    }
    
    public async void Leave()
    {
        _cancellationTokenA = true;
        
        await _networkClient.Leave();
        CurrentRoom = null;
        _cancellationTokenB = false;
    }

    public async void Send(string message) => await _networkClient.Send(message);
    public async Task<Guid> CreateRoom(string roomName, string? password, int capacity) =>
        await _networkClient.CreateRoom(roomName, password, capacity);
    
    private async void GetNewMessages()
    {
        while (!_cancellationTokenA)
        {
            Thread.Sleep(200);
            var messages = await _networkClient.GetNewMessages();
            if (messages.Length == 0) continue;
            
            foreach (var message in messages)
                _chatWriter.Write(message.Content.ToFlatString());
        }
    }

    private async void UpdateOnlineUsers()
    {
        while (!_cancellationTokenA)
        {
            Thread.Sleep(200);
            var users = await _networkClient.UpdateOnlineUsers();
            _onlineUsersWriter.Update(users);
        }
    }

    private async void UpdateRooms()
    {
        while (!_cancellationTokenB)
        {
            Thread.Sleep(200);
            var rooms = await _networkClient.UpdateRooms();
            _roomsUpdater.Update(rooms);
        }
    }
}