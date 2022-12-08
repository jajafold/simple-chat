#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private readonly ISerializer _serializer;
    private readonly IServerDataBase _dataBase;

    public UserController(IServerDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
    }

    [HttpGet]
    public JsonResult Join(Guid chatRoomId, string login)
    {
        if (_dataBase.ChatRooms[chatRoomId].RequiresPassword)
            _dataBase.Join(chatRoomId, login);
        
        var serialized = JsonConvert.SerializeObject
            (_dataBase.ChatRooms[chatRoomId].ToConfirmationModel(), Formatting.Indented);
        
        return new JsonResult(serialized);
    }

    [HttpGet]
    public JsonResult Validate(Guid chatRoomId, string login, string password)
    {
        var success = password == _dataBase.ChatRooms[chatRoomId].Password;
        if (success) _dataBase.Join(chatRoomId, login);
        
        return new JsonResult(JsonConvert.SerializeObject(
            new ConfirmationResult {Success = success}
        ));
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        _dataBase.Leave(chatRoomId, login);
    }
}