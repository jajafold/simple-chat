using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class HomeController : Controller
{
    private readonly IServerDataBase dataBase;
    private readonly ISerializer serializer;
    public HomeController(IServerDataBase dataBase, ISerializer serializer)
    {
        this.dataBase = dataBase;
        this.serializer = serializer;
    }
    [HttpGet]
    public JsonResult Index()
    {
        var result = serializer.Serialize
            (dataBase.Chatrooms.Values.ToList().ToHubsViewModel(), Formatting.Indented);
        return new JsonResult(result);
    }
}

//Message Broker a-la rabbitMQ kafka ...

