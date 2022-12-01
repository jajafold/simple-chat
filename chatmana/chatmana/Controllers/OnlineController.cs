using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private IServerDataBase dataBase;
    public OnlineController(IServerDataBase dataBase)
    {
        this.dataBase = dataBase;
    }
    public JsonResult GetUsersOnline(Guid id)
    {
        var chatId = id == default ? dataBase.MainChat : id;
        var result = JsonConvert.SerializeObject(dataBase.Chatrooms[chatId].Users);
        return new JsonResult(result);
    }
}