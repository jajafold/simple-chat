#pragma warning disable CA1416

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    public JsonResult GetUsersOnline(Guid chatRoomId)
    {
        var result = JsonConvert.SerializeObject(DataBase.ChatRooms[chatRoomId].Users);
        return new JsonResult(result);
    }
}