#pragma warning disable CA1416

using System.Globalization;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    private readonly IChatRepository _repository;
    private readonly ISerializer _serializer;

    public HomeController(IChatRepository repository, ISerializer serializer)
    {
        _repository = repository;
        _serializer = serializer;
    }

    [HttpGet]
    public JsonResult Index()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};

        var result = _serializer.Serialize
            (_repository.ChatRooms.ToHubsViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }

    [HttpGet]
    public JsonResult CreateRoom(string creatorName, string roomName, string password, int capacity)
    {
        var similarRooms = _repository.ChatRooms.Where(room => room.Name == roomName).ToList();
        var createdRoomId = _repository.AddRoom(
            creatorName, 
            similarRooms.Count == 0 ? $"{roomName}" : $"{roomName}({similarRooms.Count}) ", 
            password == "" ? null : password, 
            capacity);

        var serialized = _serializer.Serialize(createdRoomId);
        
        return new JsonResult(serialized);
    }

    [HttpGet]
    public JsonResult Ping(string timeRepresentation)
    {
        var time = DateTime.Parse(timeRepresentation, CultureInfo.InvariantCulture);
        var result = _serializer.Serialize((DateTime.Now - time).Milliseconds);
        
        return new JsonResult(result);
    }
}

//Message Broker a-la rabbitMQ kafka ...