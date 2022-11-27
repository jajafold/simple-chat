using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using chatmana.Models;
using Infrastructure;

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
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseViewModel>(serialized!);
            foreach (var name in response?.UserNames!)
                _onlineUsersWriter.Write(name);
            _currentRoom = response.RoomId;
        }

        public async void Send(string message)
        {
            var response = await _httpClient.PostAsync(
                $"{_uri}/Messages/Text?chatRoom={_currentRoom.ToString()}&name={Name}&message={message}", 
                new StringContent(""));
            var msg = await response.Content.ReadAsByteArrayAsync();
            var encoded = Encoding.Convert(Encoding.Unicode, Encoding.Default, msg);
            var text = Encoding.Default.GetString(encoded);
            _chatWriter.Write(text);
        }
    }
}