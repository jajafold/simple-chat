using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.Services;
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
}