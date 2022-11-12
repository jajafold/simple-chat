using Ninject;

namespace Chat.Infrastructure
{
    public static class DependencyInjector
    {
        public static readonly StandardKernel Injector = new();
    }
}