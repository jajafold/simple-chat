using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class OnlineControllerTests<T> where T : IDbFixture, new()
{
    private MessagesController _controller;
    private IDeserializer _deserializer;
    private T _dbFixture;
    private Func<IChatRepository> GetRepository { get; set; }
    
    [SetUp]
    public void Setup()
    {
        _dbFixture = new T();
        _controller = _dbFixture.ServiceProvider.GetService<MessagesController>();
        _deserializer = _dbFixture.ServiceProvider.GetService<IDeserializer>();
        GetRepository = () => _dbFixture.ServiceProvider.GetService<IRepositoryGenerator>().ConfigureRepository();
    }

    [Test]
    public void MessageIsPostedAndReturned()
    {
        var repository = GetRepository();
        var (text, name) = ("buba", "Vasya");
        var chatId = repository.ChatRooms.First().Id;
        _controller.Text(text, name, chatId);
        Assert.IsTrue(repository.AllMessages.Any(x => x.Name == name && x.Text == text));
    }

    //TODO : self-made exceptions
    [Test]
    public void TextThrowsInvalidOperationExceptionIfChatNotExists()
    {
        var db = GetRepository();
        var (text, name) = ("buba", "Vasya");
        Assert.Throws<InvalidOperationException>
            (()=> _controller.Text(text, name, Guid.NewGuid()));
    }

    //TODO : self-made exceptions
    [Test]
    public void TextThrowsInvalidOperationExceptionIfUserNotExists()
    {
        var db = GetRepository();
        var (text, name) = ("buba", "Vasyanich");
        Assert.Throws<InvalidOperationException>
            (()=> _controller.Text(text, name, db.ChatRooms.First().Id));
    }
}