#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private readonly ISerializer _serializer;
    private readonly IServerDataBase _dataBase;

    public OnlineController(IServerDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
    }

    public JsonResult GetUsersOnline(Guid chatRoomId)
    {
        var result = JsonConvert.SerializeObject(_dataBase.ChatRooms[chatRoomId].Users);
        return new JsonResult(result);
    }
}