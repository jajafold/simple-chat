using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Services;
using Ninject;

namespace ServerTests;

public class OnlineControllerTests
{
    private StandardKernel _container;

    [SetUp]
    public void Setup()
    {
        _container = new StandardKernel();
        _container.Bind<IServerDataBase>().ToConstant(new OnlineDataBaseGenerator().DataBase).InSingletonScope();
        _container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        _container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }

    //TODO: self-made exceptions
    [Test]
    public void NonExistingGuidThrowsException()
    {
        var controller = _container.Get<OnlineController>();
        Assert.Throws<KeyNotFoundException>(() => controller.GetUsersOnline(Guid.NewGuid()));
    }

    [Test]
    public void NotSameResultsOnNotSameRooms()
    {
        var controller = _container.Get<OnlineController>();
        var db = _container.Get<IServerDataBase>();
        var mainChatResult = controller.GetUsersOnline(db.MainChat).Value;
        var otherChatResult = controller.GetUsersOnline(Guid.Empty).Value;
        Assert.AreNotEqual(mainChatResult, otherChatResult);
    }

    [Test]
    public void CorrectContentAfterDeserialization()
    {
        var controller = _container.Get<OnlineController>();
        var db = _container.Get<IServerDataBase>();
        var mainChatResult = _container.Get<IDeserializer>()
            .Deserialize<List<string>>(controller.GetUsersOnline(db.MainChat).Value!.ToString()!);
        CollectionAssert.AreEqual(db.Chatrooms[db.MainChat].Users, mainChatResult);
    }
}