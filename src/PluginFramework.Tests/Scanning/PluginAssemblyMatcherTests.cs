using PluginFramework.Scanning;
using PluginFramework.Validation;
using System;
using System.Reflection;
using Xunit;

namespace PluginFramework.Tests.Scanning
{
    public class PluginAssemblyMatcherTests
    {
        [Fact]
        public void ReturnsValidationErrorForPluginWithConstructorParameters()
        {
            var pluginAssemblyMatcher = new PluginAssemblyMatcher<Input, Output>(new Validator());
            var plugins = pluginAssemblyMatcher.FindMatchingPlugins(
                typeof(PluginAssemblyMatcherTests).GetTypeInfo().Assembly,
                out var validationErrors);

            Assert.Contains(typeof(PluginWithConstructorParameters), validationErrors.Keys);
        }
    }

    public class PluginWithConstructorParameters : IPlugin<Input, Output>
    {
        public PluginWithConstructorParameters(string whoopsGotAParameter)
        {
        }

        public Output Execute(Input input)
        {
            throw new NotImplementedException();
        }
    }

    public class Input
    {
    }

    public class Output
    {
    }
}
