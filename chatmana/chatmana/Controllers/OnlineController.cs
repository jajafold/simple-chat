using Infrastructure;
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

    public JsonResult GetUsersOnline(Guid id)
    {
        var chatId = id == default ? dataBase.MainChat : id;
        var result = serializer.Serialize(dataBase.Chatrooms[chatId].Users);
        return new JsonResult(result);
    }
}