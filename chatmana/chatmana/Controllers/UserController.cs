#pragma warning disable CA1416

using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class UserController : Controller
{
    [HttpGet]
    public JsonResult Join(Guid chatRoomId, string login)
    {
        DataBase.Join(chatRoomId, login);
        var result = JsonConvert.SerializeObject
            (DataBase.ChatRooms[chatRoomId].Users.ToResponseViewModel(chatRoomId), Formatting.Indented);
        return new JsonResult(result);
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        DataBase.Leave(chatRoomId, login);
    }
}