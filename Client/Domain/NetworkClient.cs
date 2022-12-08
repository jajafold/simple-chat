#pragma warning disable CA1416

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.Uri;
using Newtonsoft.Json;
using static Infrastructure.Exceptions.Retry;
using UriBuilder = Infrastructure.Uri.UriBuilder;

namespace Chat.Domain;

public class NetworkClient
{
    public ClientConnectionState ConnectionState { get; private set; }
    public Guid? CurrentRoom { get; private set; }

    private readonly string _login;
    private readonly string _host;
    private readonly HttpClient _httpClient = new ();
    
    private long _lastUpdated;

    public NetworkClient(string host, string login)
    {
        _host = host;
        _login = login;
        _lastUpdated = DateTime.Now.Ticks;
    }
    
    public async Task<ConfirmationModel> TryJoin(Guid chatRoomId)
    {
        var executor = new Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("User")
            .AddFragment("Join")
            .AddQuery("chatRoomId", chatRoomId)
            .AddQuery("login", _login)
            .ToString();
        
        var result = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            throw new JoinChatRoomException("The connection was not established");
        }
            
        var response = JsonConvert.DeserializeObject<ConfirmationModel>(result);
        if (!response.NeedsConfirmation)
        {
            CurrentRoom = response.RoomId;
        }

        return response;
    }

    public async Task<ConfirmationResult> ValidatePassword(Guid chatRoomId, string? password)
    {
        var executor = new Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("User")
            .AddFragment("Validate")
            .AddQuery("chatRoomId", chatRoomId)
            .AddQuery("login", _login)
            .AddQuery("password", password ?? "")
            .ToString();
        
        var result = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            throw new JoinChatRoomException("The connection was not established");
        }

        var validation = JsonConvert.DeserializeObject<ConfirmationResult>(result);
        if (validation.Success) CurrentRoom = chatRoomId;
        
        return validation;
    }

    public async Task Send(string message)
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Messages")
            .AddFragment("Text")
            .AddQuery("chatRoomId", 
                CurrentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .AddQuery("name", _login)
            .AddQuery("message", message)
            .ToString();
        
        var response = await executor.Execute(
            async () => await _httpClient.PostAsync(uri, new StringContent("")));
        
        if (!executor.FinishedSuccessfully)
        {
            throw new SendMessageException("Connection lost");
        }
    }
    
    public async Task<MessageViewModel<IMessage>[]> GetNewMessages()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var executor = new Retry.Executor();
        
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Messages")
            .AddFragment("ChatRoomMessages")
            .AddQuery("timestamp", _lastUpdated)
            .AddQuery("chatRoomId", 
                CurrentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .ToString();
        
        var serialized = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            if (executor.Exception is ConnectionException)
                throw new ConnectionException("Connection lost");
            
            throw new Exception(
                $"Unhandled exception inside the {nameof(GetNewMessages)} executor: {executor.Exception}");
        }
        
        var response = JsonConvert.DeserializeObject<MessagesViewModel>(serialized!, settings);
        if (response.Messages.Length == 0) return Array.Empty<MessageViewModel<IMessage>>();
            
        _lastUpdated = DateTime.Now.Ticks;
        return response.Messages;
    }
    
    public async Task<string[]> UpdateOnlineUsers()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Online")
            .AddFragment("GetUsersOnline")
            .AddQuery("chatRoomId",
                CurrentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .ToString();
        
        var serialized = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            if (executor.Exception is ConnectionException)
                throw new ConnectionException("Connection lost");

            throw new Exception(
                $"Unhandled exception inside the {nameof(UpdateOnlineUsers)} executor: {executor.Exception}");
        }
        
        var users = JsonConvert.DeserializeObject<IEnumerable<string>>(serialized!, settings);
        return users.ToArray();
    }

    public async Task<RoomViewModel[]> UpdateRooms()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Home")
            .AddFragment("Index")
            .ToString();

        var serialized = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));
        
        if (!executor.FinishedSuccessfully)
        {
            if (executor.Exception is ConnectionException)
                throw new ConnectionException("Connection lost");

            throw new Exception(
                $"Unhandled exception inside the {nameof(UpdateRooms)} executor: {executor.Exception}");
        }

        var rooms = JsonConvert.DeserializeObject<RoomsViewModel>(serialized, settings);
        return rooms.ChatRooms;
    }

    public async Task<Guid> CreateRoom(string roomName, string? password, int capacity)
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("Home")
            .AddFragment("CreateRoom")
            .AddQuery("creatorName", _login)
            .AddQuery("roomName", roomName)
            .AddQuery("password", password ?? "")
            .AddQuery("capacity", capacity)
            .ToString();

        var serialized = await executor.Execute(
            async () => await _httpClient.GetFromJsonAsync<string>(uri));

        if (!executor.FinishedSuccessfully)
        {
            if (executor.Exception is ConnectionException)
                throw new ConnectionException("Connection lost");
            throw new Exception(
                $"Unhandled exception inside the {nameof(CreateRoom)} executor: {executor.Exception}");
        }

        var createdRoomId = JsonConvert.DeserializeObject<Guid>(serialized);
        CurrentRoom = createdRoomId;
        
        return createdRoomId;
    }

    public async Task Leave()
    {
        var executor = new Retry.Executor();
        var uri = new UriBuilder()
            .AddHost(_host)
            .AddFragment("User")
            .AddFragment("Leave")
            .AddQuery("chatRoomId",
                CurrentRoom ?? throw new InvalidOperationException("The room does not contain a value"))
            .AddQuery("login", _login)
            .ToString();
        
        var response = await executor.Execute(
            async () => await _httpClient.PostAsync(uri, new StringContent("")));
        
        if (!executor.FinishedSuccessfully)
            throw new ConnectionException("Connection lost");
    }
}