#pragma warning disable CA1416

using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public JsonResult Index()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        
        var result = JsonConvert.SerializeObject
            (DataBase.ChatRooms.Values.ToList().ToHubsViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }

    [HttpPost]
    public void CreateRoom(string creatorName, string roomName)
    {
        DataBase.AddRoom(creatorName, roomName);
    }
}

//Message Broker a-la rabbitMQ kafka ...

