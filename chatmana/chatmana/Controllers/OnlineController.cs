#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class OnlineController : Controller
{
    private readonly IChatRepository _repository;
    private readonly ISerializer _serializer;

    public OnlineController(IChatRepository repository, ISerializer serializer)
    {
        _repository = repository;
        _serializer = serializer;
    }

    public JsonResult GetUsersOnline(Guid chatRoomId)
    {
        var result = _serializer.Serialize
            (_repository.GetRoomById(chatRoomId).Users);
        return new JsonResult(result);
    }
}