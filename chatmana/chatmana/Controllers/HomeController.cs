#pragma warning disable CA1416

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
            (_repository.AllChatRooms.ToHubsViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }

    [HttpGet]
    public JsonResult CreateRoom(string creatorName, string roomName, string password, int capacity)
    {
        var createdRoomId = _repository.AddRoom(creatorName, roomName, password == "" ? null : password, capacity);
        var serialized = _serializer.Serialize(createdRoomId);
        return new JsonResult(serialized);
    }
}

//Message Broker a-la rabbitMQ kafka ...