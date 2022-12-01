using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public JsonResult Index()
    {
        var result = JsonConvert.SerializeObject
            (DataBase.Chatrooms.Values.ToList().ToHubsViewModel(), Formatting.Indented);
        return new JsonResult(result);
    }
}

//Message Broker a-la rabbitMQ kafka ...

