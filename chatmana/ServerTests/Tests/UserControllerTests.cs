using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class UserDbTests<T> : DbTests<T> where T : IDbFixture, new()
{
    private UserController _controller;
    
    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _controller = _dbFixture.ServiceProvider.GetService<UserController>();
    }
    
    [Test]
    public void JoinUserIsAdded()
    {
        var repository = GetRepository();
        var room = repository.AllChatRooms.First().Id;
        var name = "Petrovich";
        _controller.Validate(room, name, null);
        Assert.Contains(name, repository.GetRoomById(room).Users);
    }

    [Test]
    public void JoinReturnsRightResponseViewModel()
    {
        var repository = GetRepository();
        var name = "Bitch";
        var room = repository.AllChatRooms.First().Id;
        var result = _deserializer
            .Deserialize<ConfirmationModel>(_controller.Join(room, name).Value.ToString());
        Assert.AreEqual(room, result.RoomId);
        Assert.AreEqual(false, result.NeedsConfirmation);
    }

    [Test]
    public void JoinResponseModelsAreNotEqual()
    {
        var repository = GetRepository();
        var room = repository.AllChatRooms.First().Id;
        var nameOne = "Klava";
        var nameTwo = "Koka";
        var resultMain = _deserializer
            .Deserialize<ResponseViewModel>(_controller.Join(room, nameOne).Value.ToString());
        var resultEmpty = _deserializer
            .Deserialize<ResponseViewModel>(_controller.Join(room, nameTwo).Value.ToString());
        Assert.AreNotEqual(resultEmpty, resultMain);
    }

    //TODO: self-made exceptions
    [Test]
    public void JoinThrowsInvalidOperationExceptionIfNameExists()
    {
        var repository = GetRepository();
        var name = "Dima";
        Assert.Throws<InvalidOperationException>(() => _controller.Join(repository.AllChatRooms.First().Id, name));
    }

    //TODO : self-made exceptions
    [Test]
    public void JoinThrowsInvalidOperationExceptionIfRoomDoesNotExists()
    {
        var name = "Gena";
        Assert.Throws<InvalidOperationException>(() => _controller.Join(Guid.NewGuid(), name));
    }

    [Test]
    public void UserLeavesFromRoom()
    {
        var repository = GetRepository();
        var name = "Dima";
        var room = repository.AllChatRooms.First().Id;
        _controller.Leave(room, name);
        Assert.That(repository.GetRoomById(room).Users, Has.No.Member(name));
    }

    //TODO : self-made exceptions
    [Test]
    public void LeaveThrowsInvalidOperationExceptionIfRoomDoesNotExists()
    {
        var repository = GetRepository();
        var name = "Gena";
        Assert.Throws<InvalidOperationException>(() => _controller.Leave(Guid.NewGuid(), name));
    }
}