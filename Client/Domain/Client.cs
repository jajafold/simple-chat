#pragma warning disable CA1416

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.Models;    
using Infrastructure.Updater;
using Infrastructure.Uri;
using Newtonsoft.Json;
using UriBuilder = Infrastructure.Uri.UriBuilder;

namespace Chat.Domain;

public class Client
{
    public string Name { get; }

    private readonly HttpClient _httpClient = new();
    public ClientConnectionState ConnectionState { get; private set; }
    
    private readonly Writer _chatWriter;
    private readonly Updater<string> _onlineUsersWriter;
    private readonly string _host;
    private Guid? _currentRoom = null;

    private readonly Thread _receiveUpdate;
    private readonly Thread _onlineUsersUpdate;

    public Client(string host, string name, Writer chatWriter, Updater<string> onlineUsersWriter)
    {
        Name = name;
        ConnectionState = ClientConnectionState.Disconnected;
        ClientConnection.NetworkStatusChange += ChangeConnectionStatus;
        
        _host = host;
        _chatWriter = chatWriter;
        _onlineUsersWriter = onlineUsersWriter;

        _receiveUpdate = new Thread(GetNewMessages);
        _onlineUsersUpdate = new Thread(UpdateOnlineUsers);
    }
    
    private void ChangeConnectionStatus(ClientConnectionEventArgs e)
    {
        ConnectionState = e.State;
        //_chatWriter.Write(ConnectionState.ToString());
    } 

    public async void Join(Guid chatRoomId)
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("User")
            .AddFragment("Join")
            .AddQuery("chatRoomId", chatRoomId)
            .AddQuery("login", Name)
            .ToString();
        
        var result = await executor.Execute(
             async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            throw new JoinChatRoomException("The connection was not established");
        }
            
        var response = JsonConvert.DeserializeObject<ResponseViewModel>(result);
        _currentRoom = response.RoomId;

        _receiveUpdate.Start();
        _onlineUsersUpdate.Start();
    }

    public async void Send(string message)
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Messages")
            .AddFragment("Text")
            .AddQuery("chatRoomId", 
                _currentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .AddQuery("name", Name)
            .AddQuery("message", message)
            .ToString();
        
        var response = await executor.Execute(
            async () => await _httpClient.PostAsync(uri, new StringContent("")));
        
        if (!executor.FinishedSuccessfully)
        {
            throw new SendMessageException("Connection lost");
        }
    }

    private async void GetNewMessages()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var lastUpdated = DateTime.Now.Ticks;
        var executor = new Retry.Executor();

        while (true)
        {
            var uri = new UriBuilder()
                .AddHost(_host)
                .AddFragment("Messages")
                .AddFragment("ChatRoomMessages")
                .AddQuery("timestamp", lastUpdated)
                .AddQuery("chatRoomId", 
                    _currentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
                .ToString();
            
            Thread.Sleep(500);
            var serialized = await executor.Execute(
                    async () => await _httpClient.GetFromJsonAsync<string>(uri));

            if (!executor.FinishedSuccessfully)
            {
                throw new ConnectionException("Connection lost");
            }

            try
            {
                var response = JsonConvert.DeserializeObject<MessagesViewModel>(serialized!, settings);
                if (response.Messages.Length == 0) continue;
                
                lastUpdated = DateTime.Now.Ticks;
                foreach (var msg in response.Messages)
                    _chatWriter.Write(msg.Content.ToFlatString());
            }
            catch (Exception e)
            {
                throw new Exception("1");
            }
        }
    }

    private async void UpdateOnlineUsers()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Online")
            .AddFragment("GetUsersOnline")
            .AddQuery("chatRoomId",
                _currentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .ToString();
        
        while (true)
        {
            Thread.Sleep(500);
            var serialized = await executor.Execute(
                async () => await _httpClient.GetFromJsonAsync<string>(uri));

            if (!executor.FinishedSuccessfully)
            {
                throw new ConnectionException("Connection lost");
            }

            try
            {
                var users = JsonConvert.DeserializeObject<IEnumerable<string>>(serialized!, settings);
                _onlineUsersWriter.Update(users);
            }
            catch (Exception e)
            {
                throw new Exception("2");
            }
        }
    }

    public async void Leave()
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("User")
            .AddFragment("Leave")
            .AddQuery("chatRoomId",
                _currentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .AddQuery("login", Name)
            .ToString();
        
        var response = await executor.Execute(
            async () => await _httpClient.PostAsync(uri, new StringContent("")));
        
        if (!executor.FinishedSuccessfully)
            throw new ConnectionException("Connection lost");
    }
}