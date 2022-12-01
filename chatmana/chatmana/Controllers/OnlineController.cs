using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private IServerDataBase dataBase;
    private readonly ISerializer serializer;
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