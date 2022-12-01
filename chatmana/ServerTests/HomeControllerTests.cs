using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Ninject;

namespace ServerTests;

public class HomeControllerTests
{
    private StandardKernel Container;

    [SetUp]
    public void Setup()
    {
        Container = new StandardKernel();
        Container.Bind<IServerDataBase>().ToConstant(HomeDataBaseGenerator.DataBase);
        Container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        Container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }

    [Test]
    public void ResultNotNull()
    {
        var controller = Container.Get<HomeController>();
        var result = controller.Index().Value;
        Assert.IsNotNull(result);
    }

    [Test]
    public void ResultDeserializes()
    {
        var controller = Container.Get<HomeController>();
        var result = controller.Index().Value;
        var viewModel = Container.Get<Deserializer>().Deserialize<RoomsViewModel>(result.ToString());
        Assert.IsNotNull(viewModel);
    }

    [Test]
    public void ContentEqualsDataBase()
    {
        var controller = Container.Get<HomeController>();
        var result = controller.Index().Value;
        var viewModel = Container.Get<Deserializer>().Deserialize<RoomsViewModel>(result.ToString());
        Assert.AreEqual(viewModel.ChatRooms.Length, HomeDataBaseGenerator.DataBase.Chatrooms.Count);
        Assert.AreEqual(viewModel.ChatRooms[0].Id, HomeDataBaseGenerator.DataBase.MainChat);
        Assert.AreEqual(viewModel.ChatRooms[0].ActiveUsers,
            HomeDataBaseGenerator.DataBase.Chatrooms[HomeDataBaseGenerator.DataBase.MainChat].Users.Count);
    }
}