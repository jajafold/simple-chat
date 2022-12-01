using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private IServerDataBase dataBase;
    public UserController(IServerDataBase dataBase)
    {
        this.dataBase = dataBase;
    }
    [HttpGet]
    public JsonResult Join(Guid chatroomId, string login)
    {
        dataBase.Join(dataBase.MainChat, login);
        var result = JsonConvert.SerializeObject
            (dataBase.Chatrooms[dataBase.MainChat].Users.ToResponseViewModel(dataBase.MainChat), Formatting.Indented);
        return new JsonResult(result);
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        dataBase.Leave(chatRoomId, login);
    }
}