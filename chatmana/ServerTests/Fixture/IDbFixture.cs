using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

public interface IDbFixture
{
    public ServiceProvider ServiceProvider { get; set; }
}