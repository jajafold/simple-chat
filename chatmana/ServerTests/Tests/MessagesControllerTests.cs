using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class MessagesControllerTests<T> : ControllerTests<T> where T : IDbFixture, new()
{
    private MessagesController _controller;
    
    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _controller = _dbFixture.ServiceProvider.GetService<MessagesController>()!;
    }

    [Test]
    public void MessageIsPostedAndReturned()
    {
        var repository = GetRepository();
        var (text, name) = ("buba", "Vasya");
        var chatId = repository.ChatRooms.First().Id;
        _controller.Text(text, name, chatId);
        Assert.That(repository.AllMessages.Any(x => x.Name == name && x.Text == text), Is.True);
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