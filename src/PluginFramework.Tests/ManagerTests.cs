using PluginFramework.Example;
using System;
using System.Linq;
using Xunit;

namespace PluginFramework.Tests
{
    public class ManagerTests
    {
        [Fact]
        public void CanScanForAndExecutePluginsInThisAssembly()
        {
            var pluginManager = PluginManager.ForPluginTypes<ExampleInput, ExampleOutput>();
            pluginManager.ScanForPlugins(Environment.CurrentDirectory, "PluginFramework.Example.dll");

            var examplePlugin = pluginManager.Plugins.SingleOrDefault();
            var output = examplePlugin.Execute(new ExampleInput());

            Assert.True(output.HasExecuted);
        }
    }
}
