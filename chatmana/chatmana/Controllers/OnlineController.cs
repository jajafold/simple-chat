#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private readonly IDataBase _dataBase;
    private readonly ISerializer _serializer;

    public OnlineController(IDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
    }

    public JsonResult GetUsersOnline(Guid chatRoomId)
    {
        var result = _serializer.Serialize
            (_dataBase.GetRoomById(chatRoomId).Users);
        return new JsonResult(result);
    }
}