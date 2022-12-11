using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public class OnlineControllerTests<T> : ControllerTests<T> where T : IDbFixture, new()
{
    private OnlineController _controller;
    
    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _controller = _dbFixture.ServiceProvider.GetService<OnlineController>();
    }
    
}