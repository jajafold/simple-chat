using chatmana.Services;
using Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Infrastructure.Models;

namespace chatmana.Controllers;

public class MessagesController : Controller
{
    [HttpPost]
    public string Text(string message, string name, Guid chatRoom)
    {
        var msg = new TextMessage(message, chatRoom, DateTime.Now, name);
        DataBase.PostMessage(msg);
        return $"{msg.ToFlatString()}";
    }

    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatRoomId)
    {
        var result = JsonConvert.SerializeObject(DataBase.Messages
            .Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp < timestamp - 100)
            .ToMessagesViewModel(), Formatting.Indented);
        return new JsonResult(result);
    }   
}