#pragma warning disable CA1416

using Infrastructure.Services;
using Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Infrastructure.Models;

namespace chatmana.Controllers;

public class MessagesController : Controller
{
    [HttpPost]
    public void Text(string message, string name, Guid chatRoomId)
    {
        DataBase.PostMessage(new TextMessage(message, chatRoomId, DateTime.Now, name));
    }

    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatRoomId)
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var result = JsonConvert.SerializeObject(DataBase.Messages
            .Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp > timestamp)
            .ToMessagesViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }   
}