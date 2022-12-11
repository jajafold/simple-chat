using chatmana.Controllers;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

public class ChatTestFixture : IDbFixture
{
    public ChatTestFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddDbContext<ChatDbContext>(o => o.UseSqlite("DataSource=file::memory:?cache=shared"),
                ServiceLifetime.Transient);
        serviceCollection.AddTransient<HomeController, HomeController>();
        serviceCollection.AddTransient<MessagesController, MessagesController>();
        serviceCollection.AddTransient<UserController, UserController>();
        serviceCollection.AddTransient<OnlineController, OnlineController>();
        serviceCollection.AddTransient<ISerializer, Serializer>();
        serviceCollection.AddTransient<IDeserializer, Deserializer>();
        serviceCollection.AddTransient<ChatDbContext, ChatDbContext>();
        serviceCollection.AddTransient<IChatRepository, ChatRepository>();
        serviceCollection.AddTransient<IRepositoryGenerator, RepositoryGenerator>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; set; }
}