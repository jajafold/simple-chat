using chatmana.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    public JsonResult GetUsersOnline(Guid id)
    {
        DataBase.Join(DataBase.MainChat, "vasilich");
        var result = JsonConvert.SerializeObject(DataBase.Chatrooms[DataBase.MainChat].Users);
        return new JsonResult(result);
    }
}