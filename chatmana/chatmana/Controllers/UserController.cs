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
        DataBase.Join(DataBase.MainChat, login);
        var result = JsonConvert.SerializeObject
            (DataBase.Chatrooms[DataBase.MainChat].Users.ToResponseViewModel(DataBase.MainChat), Formatting.Indented);
        return new JsonResult(result);
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        DataBase.Leave(chatRoomId, login);
    }
}