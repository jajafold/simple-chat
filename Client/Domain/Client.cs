using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.Models;    
using Infrastructure.Updater;
using Newtonsoft.Json;

namespace Chat.Domain;

public class Client
{
    public string Name { get; }

    private readonly HttpClient _httpClient = new();
    public ClientConnectionState ConnectionState { get; private set; }
    
    private readonly Writer _chatWriter;
    private readonly Updater<string> _onlineUsersWriter;
    private readonly string _uri;
    private Guid? _currentRoom = null;

    private readonly Thread _receiveUpdate;
    private readonly Thread _onlineUsersUpdate;

    public Client(string uri, string name, Writer chatWriter, Updater<string> onlineUsersWriter)
    {
        Name = name;
        _uri = uri;
        _chatWriter = chatWriter;
        _onlineUsersWriter = onlineUsersWriter;

        _receiveUpdate = new Thread(GetNewMessages);
        _onlineUsersUpdate = new Thread(UpdateOnlineUsers);
    }

    public async void Join(Guid chatRoomId)
    {
        var result = await Retry.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(
                $"{_uri}/User/Join?chatroomId={chatRoomId.ToString()}&login={Name}"),
            args => ConnectionState = args.State,
            out var e
        )!;

        if (result is null)
        {
            ConnectionState = ClientConnectionState.Disconnected;
            throw new JoinChatRoomException("The connection was not established", e);
        }
            
        var response = JsonConvert.DeserializeObject<ResponseViewModel>(result);
        _currentRoom = response.RoomId;

        ThreadPool.QueueUserWorkItem(state => _receiveUpdate.Start());
        ThreadPool.QueueUserWorkItem(state => _onlineUsersUpdate.Start());
    }

    public async void Send(string message)
    {
        var result = await Retry.Execute(
            async () => await _httpClient.PostAsync(
                $"{_uri}/Messages/Text?chatRoom={_currentRoom.ToString()}&name={Name}&message={message}",
                new StringContent("")),
            args => ConnectionState = args.State,
            out var e)!;

        if (result is null)
        {
            throw new SendMessageException("Connection lost", e);
        }
    }

    private async void GetNewMessages()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var lastUpdated = DateTime.Now.Ticks;
            
        while (true)
        {
            Thread.Sleep(500);
            var serialized = await _httpClient.GetFromJsonAsync<string>(
                $"{_uri}/Messages/ChatRoomMessages?timestamp={lastUpdated}&chatRoomId={_currentRoom.ToString()}"
            );
            var response = JsonConvert.DeserializeObject<MessagesViewModel>(serialized!, settings);
            if (response.Messages.Length == 0) continue;
                
            lastUpdated = DateTime.Now.Ticks;
            foreach (var msg in response.Messages)
                _chatWriter.Write(msg.Content.ToFlatString());
        }
    }

    public async void UpdateOnlineUsers()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        while (true)
        {
            Thread.Sleep(500);
            var serialized = await _httpClient.GetFromJsonAsync<string>(
                $"{_uri}/Online/GetUsersOnline?chatRoomId={_currentRoom.ToString()}");
            var users = JsonConvert.DeserializeObject<IEnumerable<string>>(serialized!, settings);
            _onlineUsersWriter.Update(users);
        }
    }

    public async void Leave()
    {
        var response = await _httpClient.PostAsync(
            $"{_uri}/User/Leave?chatRoomId={_currentRoom.ToString()}&login={Name}",
            new StringContent(""));
    }
}