using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Ninject;

namespace ServerTests;

public class UserControllerTests
{
    private StandardKernel _container;

    [SetUp]
    public void Setup()
    {
        _container = new StandardKernel();
        _container.Bind<IServerDataBase>().ToConstant(new UserDataBaseGenerator().DataBase);
        _container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        _container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }

    [Test]
    public void JoinUserIsAdded()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Klava";
        controller.Join(db.MainChat, name);
        Assert.Contains(name, db.Chatrooms[db.MainChat].Users);
    }
    
    [Test]
    public void JoinReturnsRightResponseViewModel()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Klava";
        var result = _container.Get<IDeserializer>()
            .Deserialize<ResponseViewModel>(controller.Join(db.MainChat, name).Value.ToString());
        Assert.AreEqual(db.MainChat, result.RoomId);
        CollectionAssert.AreEquivalent(db.Chatrooms[db.MainChat].Users, result.UserNames);
    }
    
    [Test]
    public void JoinResponseModelsAreNotEqual()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Klava";
        var resultMain = _container.Get<IDeserializer>()
            .Deserialize<ResponseViewModel>(controller.Join(db.MainChat, name).Value.ToString());
        var resultEmpty = _container.Get<IDeserializer>()
            .Deserialize<ResponseViewModel>(controller.Join(Guid.Empty, name).Value.ToString());
        Assert.AreNotEqual(resultEmpty, resultMain);
    }

    //TODO: self-made exceptions
    [Test]
    public void JoinThrowsInvalidOperationExceptionIfNameExists()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Gena";
        Assert.Throws<InvalidOperationException>(() => controller.Join(db.MainChat, name));
    }
    
    //TODO : self-made exceptions
    [Test]
    public void JoinThrowsKeyNotFoundExceptionIfRoomDoesNotExists()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Gena";
        Assert.Throws<KeyNotFoundException>(() => controller.Join(Guid.NewGuid(), name));
    }

    [Test]
    public void UserLeavesFromRoom()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Gena";
        controller.Leave(db.MainChat, name);
        Assert.That(db.Chatrooms[db.MainChat].Users, Has.No.Member(name));
    }
    
    //TODO : self-made exceptions
    [Test]
    public void LeaveThrowsKeyNotFoundExceptionIfRoomDoesNotExists()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Gena";
        Assert.Throws<KeyNotFoundException>(() => controller.Leave(Guid.NewGuid(), name));
    }
    
    //TODO: self-made exceptions
    [Test]
    public void LeaveThrowsInvalidOperationExceptionIfNameNotExists()
    {
        var controller = _container.Get<UserController>();
        var db = _container.Get<IServerDataBase>();
        var name = "Mamasha";
        Assert.Throws<InvalidOperationException>(() => controller.Leave(db.MainChat, name));
    }
}