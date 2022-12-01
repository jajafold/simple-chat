using Ninject;

namespace Infrastructure
{
    public static class DependencyInjector
    {
        public static readonly StandardKernel Injector = new();
    }
}