using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class HomeDbTests<T> : DbTests<T> where T : IDbFixture, new()
{
    private HomeController _controller;
    
    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _controller = _dbFixture.ServiceProvider.GetService<HomeController>()!;
    }

    [Test]
    public void ResultNotNull()
    {
        var result = _controller.Index().Value;
        Assert.IsNotNull(result);
    }

    [Test]
    public void ResultDeserializes()
    {
        var result = _controller.Index().Value;
        var viewModel = _deserializer.Deserialize<RoomsViewModel>(result.ToString());
        Assert.IsNotNull(viewModel);
    }

    [Test]
    public void ContentEqualsDataBase()
    {
        var db = GetRepository();
        var result = _controller.Index().Value;
        var viewModel = _deserializer.Deserialize<RoomsViewModel>(result.ToString());
        Assert.That(db.AllChatRooms.Count(), Is.EqualTo(viewModel.ChatRooms.Length));
        Assert.That(db.AllChatRooms.First().Id, Is.EqualTo(viewModel.ChatRooms[0].Id));
        Assert.That(viewModel.ChatRooms[0].ActiveUsers, Is.EqualTo(db.AllChatRooms.First().Users.Count));
    }
}