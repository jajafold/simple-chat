﻿#pragma warning disable CA1416

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

    [HttpGet]
    public JsonResult CreateRoom(string creatorName, string roomName, string password, int capacity)
    {
        var createdRoom = DataBase.AddRoom(creatorName, roomName, password == "" ? null : password, capacity);
        var serialized = JsonConvert.SerializeObject(createdRoom);
        return new JsonResult(serialized);
    }
}

//Message Broker a-la rabbitMQ kafka ...

