using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    private IServerDataBase dataBase;
    public HomeController(IServerDataBase dataBase)
    {
        this.dataBase = dataBase;
    }
    [HttpGet]
    public JsonResult Index()
    {
        var result = JsonConvert.SerializeObject
            (dataBase.Chatrooms.Values.ToList().ToHubsViewModel(), Formatting.Indented);
        return new JsonResult(result);
    }
}

//Message Broker a-la rabbitMQ kafka ...

