#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private readonly IDataBase _dataBase;
    private readonly ISerializer _serializer;

    public UserController(IDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
    }

    [HttpGet]
    public JsonResult Join(Guid chatRoomId, string login)
    {
        if (_dataBase.GetRoomById(chatRoomId).Password != null)
            _dataBase.Join(chatRoomId, login);

        var serialized = _serializer.Serialize
            (_dataBase.GetRoomById(chatRoomId).ToConfirmationModel());

        return new JsonResult(serialized);
    }

    [HttpGet]
    public JsonResult Validate(Guid chatRoomId, string login, string password)
    {
        var success = password == _dataBase.GetRoomById(chatRoomId).Password;
        if (success) _dataBase.Join(chatRoomId, login);

        return new JsonResult(_serializer.Serialize(
            new ConfirmationResult {Success = success}
        ));
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        _dataBase.Leave(chatRoomId, login);
    }
}