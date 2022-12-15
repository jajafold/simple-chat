using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class OnlineDbTests<T> : DbTests<T> where T : IDbFixture, new()
{
    private OnlineController _controller;
    
    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _controller = _dbFixture.ServiceProvider.GetService<OnlineController>();
    }
    
    //TODO: self-made exceptions
    [Test]
    public void NonExistingGuidThrowsException()
    {
        Assert.Pass();
        Assert.Throws<KeyNotFoundException>(() => _controller.GetUsersOnline(Guid.NewGuid()));
    }

    [Test]
    public void NotSameResultsOnNotSameRooms()
    {
        var repository = GetRepository();
        var roomOne = repository.AllChatRooms.First().Id;
        var roomTwo = repository.AllChatRooms.Last().Id;
        var mainChatResult = _controller.GetUsersOnline(roomOne).Value;
        var otherChatResult = _controller.GetUsersOnline(roomTwo).Value;
        Assert.AreNotEqual(mainChatResult, otherChatResult);
    }

    [Test]
    public void CorrectContentAfterDeserialization()
    {
        var repository = GetRepository();
        var room = repository.AllChatRooms.First().Id;
        var mainChatResult = _deserializer
            .Deserialize<List<string>>(_controller.GetUsersOnline(room).Value!.ToString()!);
        CollectionAssert.AreEqual(repository.GetRoomById(room).Users, mainChatResult);
    }
}