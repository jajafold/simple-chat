#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatmana.Controllers;

public class UserController : Controller
{
    private readonly IChatRepository _repository;
    private readonly ISerializer _serializer;

    public UserController(IChatRepository repository, ISerializer serializer)
    {
        _repository = repository;
        _serializer = serializer;
    }

    [HttpGet]
    public JsonResult Join(Guid chatRoomId, string login)
    {
        var room = _repository.GetRoomById(chatRoomId);
        if (room.Password == null)
            _repository.Join(chatRoomId, login);

        var serialized = _serializer.Serialize
            (room.ToConfirmationModel());

        return new JsonResult(serialized);
    }

    [HttpGet]
    public JsonResult Validate(Guid chatRoomId, string login, string? password)
    {
        var success = password == _repository.GetRoomById(chatRoomId).Password;
        if (success) _repository.Join(chatRoomId, login);

        return new JsonResult(_serializer.Serialize(
            new ConfirmationResult {Success = success}
        ));
    }

    [HttpPost]
    public void Leave(Guid chatRoomId, string login)
    {
        _repository.Leave(chatRoomId, login);
    }
}