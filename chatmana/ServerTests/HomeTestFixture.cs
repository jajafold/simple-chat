using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

public class HomeTestFixture : IDbFixture
{
    public HomeTestFixture()
    {
        var cnn = new SqliteConnection("Filename=:memory:");
        cnn.Open();
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddDbContext<ChatDbContext>(o => o.UseSqlite(cnn),
                ServiceLifetime.Transient);
        serviceCollection.AddTransient<HomeController, HomeController>();
        serviceCollection.AddTransient<ISerializer, Serializer>();
        serviceCollection.AddTransient<IDeserializer, Deserializer>();
        serviceCollection.AddTransient<IDataBase, ChatDbContext>();
        serviceCollection.AddTransient<IDataBaseGenerator, HomeDataBaseGenerator>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
        var a = ServiceProvider.GetService<HomeController>();
    }

    public ServiceProvider ServiceProvider { get; set; }
}