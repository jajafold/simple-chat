using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Ninject;

namespace ServerTests;

public class HomeControllerTests
{
    private StandardKernel _container;

    [SetUp]
    public void Setup()
    {
        _container = new StandardKernel();
        _container.Bind<IServerDataBase>().ToConstant(new HomeDataBaseGenerator().DataBase);
        _container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        _container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }

    [Test]
    public void ResultNotNull()
    {
        var controller = _container.Get<HomeController>();
        var result = controller.Index().Value;
        Assert.IsNotNull(result);
    }

    [Test]
    public void ResultDeserializes()
    {
        var controller = _container.Get<HomeController>();
        var result = controller.Index().Value;
        var viewModel = _container.Get<Deserializer>().Deserialize<RoomsViewModel>(result.ToString());
        Assert.IsNotNull(viewModel);
    }

    [Test]
    public void ContentEqualsDataBase()
    {
        var controller = _container.Get<HomeController>();
        var result = controller.Index().Value;
        var viewModel = _container.Get<Deserializer>().Deserialize<RoomsViewModel>(result.ToString());
        var db = _container.Get<IServerDataBase>();
        Assert.AreEqual(viewModel.ChatRooms.Length, db.ChatRooms.Count);
        Assert.AreEqual(viewModel.ChatRooms[0].Id, db.MainChat);
        Assert.AreEqual(viewModel.ChatRooms[0].ActiveUsers,
            db.ChatRooms[db.MainChat].Users!.Count);
    }
}