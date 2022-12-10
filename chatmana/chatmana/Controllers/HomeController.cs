﻿#pragma warning disable CA1416

using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    private readonly IServerDataBase _dataBase;
    private readonly ISerializer _serializer;

    public HomeController(IServerDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
        
        _dataBase.AddRoom("SERVER1", "MAIN1", "123", 10);
        _dataBase.AddRoom("SERVER2", "MAIN2", "123", 0);
        _dataBase.AddRoom("SERVER3", "MAIN3", null, 0);
        _dataBase.AddRoom("SERVER4", "MAIN4", null, 0);
        _dataBase.AddRoom("SERVER5", "MAIN5", null, 0);
    }

    [HttpGet]
    public JsonResult Index()
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        
        var result = JsonConvert.SerializeObject
            (_dataBase.ChatRooms.Values.ToList().ToHubsViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }

    [HttpGet]
    public JsonResult CreateRoom(string creatorName, string roomName, string password, int capacity)
    {
        var createdRoomId = _dataBase.AddRoom(creatorName, roomName, password == "" ? null : password, capacity);
        var serialized = JsonConvert.SerializeObject(createdRoomId);
        return new JsonResult(serialized);
    }
}

//Message Broker a-la rabbitMQ kafka ...