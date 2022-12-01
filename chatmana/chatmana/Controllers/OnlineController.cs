using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    public JsonResult GetUsersOnline(Guid id)
    {
        var chatId = id == default ? DataBase.MainChat : id;
        var result = JsonConvert.SerializeObject(DataBase.Chatrooms[chatId].Users);
        return new JsonResult(result);
    }
}