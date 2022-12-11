using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class HomeControllerTests<T> where T : IDbFixture, new()
{
    private HomeController _controller;
    private IDeserializer _deserializer;
    private T _dbFixture;
    private Func<IChatRepository> GetRepository { get; set; }

    [SetUp]
    public void Setup()
    {
        _dbFixture = new T();
        _controller = _dbFixture.ServiceProvider.GetService<HomeController>();
        _deserializer = _dbFixture.ServiceProvider.GetService<IDeserializer>();
        GetRepository = () => _dbFixture.ServiceProvider.GetService<IRepositoryGenerator>().ConfigureRepository();
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
        Assert.AreEqual(viewModel.ChatRooms.Length, db.ChatRooms.Count());
        Assert.AreEqual(viewModel.ChatRooms[0].Id, db.ChatRooms.First().Id);
        Assert.AreEqual(db.ChatRooms.First().Users.Count,viewModel.ChatRooms[0].ActiveUsers);
    }
}