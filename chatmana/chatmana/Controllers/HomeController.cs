using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

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
            (dataBase.Chatrooms.Values.ToList().ToHubsViewModel());
        return new JsonResult(result);
    }
}

//Message Broker a-la rabbitMQ kafka ...