using chatmana.Services;
using Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpPost]
    public JsonResult Test(string str)
    {
        return Json($"Successfully posted {str}!");
    }
    
    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatroomId)
    {
        // DataBase.Messages.Add(new TextMessage("hello",
        //     DataBase.MainChat, DateTime.Now, "vasya"));
        // DataBase.Messages.Add(new TextMessage("hellovasya", 
        //     DataBase.MainChat, DateTime.Now, "gena"));
        var result = JsonConvert.SerializeObject(DataBase.Messages
            //.Where(x => x.ChatRoom == chatroomId)
            //.Where(x => x.TimeStamp > timestamp -100)
            .ToMessagesViewModel(), Formatting.Indented);
        return new JsonResult(result);
    }
}