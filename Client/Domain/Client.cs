#pragma warning disable CA1416

using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Updater;

namespace Chat.Domain;

public class Client
{ 
    public string Name { get; }

    private readonly NetworkClient _networkClient;
    private readonly Writer _chatWriter;
    private readonly Updater<string> _onlineUsersWriter;
    
    private readonly Thread _receiveUpdate;
    private readonly Thread _onlineUsersUpdate;
    
    private bool _cancellationToken;

    public Client(string host, string name, Writer chatWriter, Updater<string> onlineUsersWriter)
    {
        Name = name;
        _networkClient = new NetworkClient(host, name);

        _chatWriter = chatWriter;
        _onlineUsersWriter = onlineUsersWriter;

        _receiveUpdate = new Thread(GetNewMessages);
        _onlineUsersUpdate = new Thread(UpdateOnlineUsers);
    }

    public async void Join(Guid chatRoomId)
    {
        await _networkClient.Join(chatRoomId);

        _cancellationToken = false;
        _receiveUpdate.Start();
        _onlineUsersUpdate.Start();
    }
    
    public async void Leave()
    {
        _cancellationToken = true;
        await _networkClient.Leave();
    }

    public async void Send(string message) => await _networkClient.Send(message);
    

    private async void GetNewMessages()
    {
        while (!_cancellationToken)
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
        while (!_cancellationToken)
        {
            Thread.Sleep(200);
            var users = await _networkClient.UpdateOnlineUsers();
            _onlineUsersWriter.Update(users);
        }
    }
}