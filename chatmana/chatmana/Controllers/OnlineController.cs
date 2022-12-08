#pragma warning disable CA1416

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private readonly ISerializer serializer;
    private readonly IServerDataBase dataBase;

    public OnlineController(IServerDataBase dataBase, ISerializer serializer)
    {
        this.dataBase = dataBase;
        this.serializer = serializer;
    }

    public JsonResult GetUsersOnline(Guid chatRoomId)
    {
        var result = JsonConvert.SerializeObject(DataBase.ChatRooms[chatRoomId].Users);
        return new JsonResult(result);
    }
}