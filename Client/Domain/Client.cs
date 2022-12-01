using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Infrastructure;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace Chat.Domain
{
    public class Client
    {
        public string Name { get; }

        private readonly HttpClient _httpClient = new();
        private readonly Writer _chatWriter;
        private readonly Writer _onlineUsersWriter;
        private readonly string _uri;
        private Guid? _currentRoom = null;

        public Client(string uri, string name, Writer chatWriter, Writer onlineUsersWriter)
        {
            Name = name;
            _uri = uri;
            _chatWriter = chatWriter;
            _onlineUsersWriter = onlineUsersWriter;
        }

        public async void Join(Guid chatRoomId)
        {
            var serialized = (string) await _httpClient.GetFromJsonAsync(
                $"{_uri}/User/Join?chatroomId={chatRoomId.ToString()}&login={Name}",
                typeof(string));
            var response = JsonConvert.DeserializeObject<ResponseViewModel>(serialized!);
            foreach (var name in response?.UserNames!)
                _onlineUsersWriter.Write(name);
            _currentRoom = response.RoomId;
            
            var receivingThread = new Thread(GetNewMessages);
            receivingThread.Start();
        }

        public async void Send(string message)
        {
            var response = await _httpClient.PostAsync(
                $"{_uri}/Messages/Text?chatRoom={_currentRoom.ToString()}&name={Name}&message={message}", 
                new StringContent(""));
        }

        public async void GetNewMessages()
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

        public async void Leave()
        {
            var response = await _httpClient.PostAsync(
                $"{_uri}/User/Leave?chatRoomId={_currentRoom.ToString()}&login={Name}",
                new StringContent(""));
        }
    }
}