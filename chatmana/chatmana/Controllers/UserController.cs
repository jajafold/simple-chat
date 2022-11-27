using chatmana.Models;
using chatmana.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class UserController : Controller
{
    [HttpGet]
    public JsonResult Join(Guid chatroomId, string login)
    {
        DataBase.Join(DataBase.MainChat, login);
        var result = JsonConvert.SerializeObject
            (DataBase.Chatrooms[DataBase.MainChat].Users.ToResponseViewModel(DataBase.MainChat), Formatting.Indented);
        return new JsonResult(result);
    }
}