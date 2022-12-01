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
        dataBase.Join(chatroomId, login);
        var result = serializer.Serialize
            (dataBase.Chatrooms[chatroomId].Users.ToResponseViewModel(chatroomId));
        return new JsonResult(result);
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        dataBase.Leave(chatRoomId, login);
    }
}