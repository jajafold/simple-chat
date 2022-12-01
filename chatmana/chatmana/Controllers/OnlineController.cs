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
        var result = serializer.Serialize(dataBase.Chatrooms[id].Users);
        return new JsonResult(result);
    }
}