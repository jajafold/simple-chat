using Infrastructure;
using Infrastructure.Services;
using Ninject;

namespace ServerTests;

public class OnlineControllerTests
{
    private StandardKernel Container;

    [SetUp]
    public void Setup()
    {
        Container = new StandardKernel();
        Container.Bind<IServerDataBase>().ToConstant(HomeDataBaseGenerator.DataBase);
        Container.Bind<ISerializer>().To<Serializer>().InSingletonScope();
        Container.Bind<IDeserializer>().To<Deserializer>().InSingletonScope();
    }

    [Test]
    public void Jopa()
    {
        Assert.Pass();
    }
}