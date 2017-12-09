using Xunit;

namespace PluginFramework.Tests.IoC
{
    public class ContainerTests
    {
        [Fact]
        public void CanInstantiatePluginManager()
        {
            var pluginManager = PluginManager.ForPluginTypes<Input, Output>();
        }
    }

    public class Input
    {
    }

    public class Output
    {
    }
}
