using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private readonly ISerializer serializer;
    private readonly IServerDataBase dataBase;

    public UserController(IServerDataBase dataBase, ISerializer serializer)
    {
        this.dataBase = dataBase;
        this.serializer = serializer;
    }

    [HttpGet]
    public JsonResult Join(Guid chatroomId, string login)
    {
        dataBase.Join(dataBase.MainChat, login);
        var result = serializer.Serialize
            (dataBase.Chatrooms[dataBase.MainChat].Users.ToResponseViewModel(dataBase.MainChat));
        return new JsonResult(result);
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        dataBase.Leave(chatRoomId, login);
    }
}