using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using Ninject;

namespace ServerTests;

public class MessagesControllerTests
{
    private StandardKernel _container;

    [SetUp]
    public void Setup()
    {
        _container = new StandardKernel();
        _container.Bind<IServerDataBase>().ToConstant(new MessagesDataBaseGenerator().DataBase);
        _container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        _container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }
    
    [Test]
    public void MessageIsPostedAndReturned()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        var (text, name) = ("buba", "Vasya");
        var result = controller.Text(text, name, db.MainChat);
        Assert.IsNotNull(result);
        Assert.IsTrue(db.Messages.Any(x => x.Text == text && x.Name == name && x.ChatRoom == db.MainChat));
    }
    
    //TODO : self-made exceptions
    [Test]
    public void TextThrowsKeyNotFoundExceptionIfChatNotExists()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        var (text, name) = ("buba", "Vasya");
        Assert.Throws<KeyNotFoundException>
            (()=> controller.Text(text, name, Guid.NewGuid()));
    }
    
    //TODO : self-made exceptions
    [Test]
    public void TextThrowsInvalidOperationExceptionIfUserNotExists()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        var (text, name) = ("buba", "Vasyanich");
        Assert.Throws<InvalidOperationException>
            (()=> controller.Text(text, name, db.MainChat));
    }

    [Test]
    public void GetMessagesDoesNotReturnOutdatedMessages()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        var (text, name) = ("buba", "Vasya");
        controller.Text(text, name, db.MainChat);
        var resultJson = controller.ChatRoomMessages(DateTime.Now.Ticks, db.MainChat).Value;
        var result = _container.Get<Deserializer>().Deserialize<MessagesViewModel>(resultJson.ToString(),
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
        Assert.AreEqual(result.Messages.Length, 0);
    }
    
    [Test]
    public void GetMessagesReturnsAllMessagesFromChat()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        var (text, name) = ("buba", "Vasya");
        for (var i = 0; i < 10; i++)
            controller.Text(text, name, db.MainChat);
        controller.Text(text, "Dima", Guid.Empty);
        var resultJson = controller.ChatRoomMessages(DateTime.MinValue.Ticks, db.MainChat).Value;
        var result = _container.Get<Deserializer>().Deserialize<MessagesViewModel>(resultJson.ToString(),
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
        Assert.AreEqual(result.Messages.Length, 10);
    }

    [Test]
    public void InvalidGuidThrowsInvalidOperationException()
    {
        var controller = _container.Get<MessagesController>();
        var db = _container.Get<IServerDataBase>();
        Assert.Throws<InvalidOperationException>
            (() =>controller.ChatRoomMessages(DateTime.Now.Ticks, Guid.NewGuid()));
    }
    
}