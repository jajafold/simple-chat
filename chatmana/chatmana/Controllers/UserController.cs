#pragma warning disable CA1416

using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private readonly ISerializer serializer;
    private readonly IServerDataBase dataBase;

    public UserController(IServerDataBase dataBase, ISerializer serializer)
    {
        this.dataBase = dataBase;
        this.serializer = serializer;
    }

    [HttpGet]
    public JsonResult Join(Guid chatRoomId, string login)
    {
        if (!DataBase.ChatRooms[chatRoomId].RequiresPassword)
            DataBase.Join(chatRoomId, login);
        
        var serialized = JsonConvert.SerializeObject
            (DataBase.ChatRooms[chatRoomId].ToConfirmationModel(), Formatting.Indented);
        
        return new JsonResult(serialized);
    }

    [HttpGet]
    public JsonResult Validate(Guid chatRoomId, string login, string password)
    {
        var success = password == DataBase.ChatRooms[chatRoomId].Password;
        if (success) DataBase.Join(chatRoomId, login);
        
        return new JsonResult(JsonConvert.SerializeObject(
            new ConfirmationResult {Success = success}
        ));
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        dataBase.Leave(chatRoomId, login);
    }
}