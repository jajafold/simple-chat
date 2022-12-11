namespace ServerTests;

[TestFixture(typeof(ChatTestFixture))]
public abstract class ControllerTests<T> where T : IDbFixture, new()
{
    
}