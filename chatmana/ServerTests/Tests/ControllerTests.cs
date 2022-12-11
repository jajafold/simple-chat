using Infrastructure;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public abstract class ControllerTests<TDbFixture> where TDbFixture : IDbFixture, new()
{
    protected IDeserializer _deserializer;
    protected TDbFixture _dbFixture;
    protected Func<IChatRepository> GetRepository { get; set; }
    
    public void Setup()
    {
        _dbFixture = new TDbFixture();
        _deserializer = _dbFixture.ServiceProvider.GetService<IDeserializer>();
        GetRepository = () => _dbFixture.ServiceProvider.GetService<IRepositoryGenerator>().ConfigureRepository();
    }
}